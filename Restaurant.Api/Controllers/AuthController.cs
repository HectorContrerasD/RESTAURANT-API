using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Restaurant.Api.Models.Entities;
using Restaurant.Api.Payloads;
using Restaurant.Api.Repositories;
using Restaurant.Api.Repositories.Abstractions;
using Restaurant.Api.Repositories.Validators;
using Restaurant.Api.Services.Abstractions;
using Restaurant.Api.Services.Utils;
using System.Diagnostics.Contracts;
using System.Security.Claims;

namespace Restaurant.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController(IConfiguration configuration, IUserRepository userRepository, IJwtService jwtService, ISesionRepository sesionRepository) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginPayload request)
        {
            try
            {
                var validator = new LoginValidator();
                var validation = await validator.ValidateAsync(request);
                if (!validation.IsValid) return BadRequest(validation.Errors.ToPayload());
                var user = await userRepository.GetUsuarioAsync(request.Username);
                if (user == null || request.Password.ToSha512String() != user.Password) return Unauthorized();
                if (!int.TryParse(configuration["Session:AccessTTL"], out int access_ttl))
                {
                    access_ttl = Constants.DefaultAccessTTL;
                }
                var access_claims = new List<Claim> {
                    new(Claims.Id, user.Id.ToString()),
                    new(Claims.Username, user.Username),
                    new(Claims.Role, user.Rol) };
                var access_token = jwtService.CreateToken(access_claims, TimeSpan.FromMinutes(access_ttl));
                if (!int.TryParse(configuration["Session:RefreshTTL"], out int refresh_ttl))
                {
                    refresh_ttl = Constants.DefaultRefreshTTL;
                }
                var refresh_claims = new Claim[] { };
                var refresh_token = jwtService.CreateToken(refresh_claims, TimeSpan.FromDays(refresh_ttl));
                var sesion = new Sesion { UsuarioId = user.Id, Hash = refresh_token.ToSHA256String() };
                await sesionRepository.InsertAsync(sesion);
                Response.Cookies.Append("refresh_token", refresh_token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(refresh_ttl)
                });
                return Ok(new AuthPayload
                {
                    AccessToken = access_token,
                });
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }

        }
        [HttpGet("refresh")]
        public async Task<IActionResult> RefreshAsync()
        {
            try
            {
                var refresh_token = Request.Cookies["refresh_token"]?.ToString();
                if (string.IsNullOrEmpty(refresh_token)) return BadRequest();
                var claimsPrincipal = jwtService.ValidateToken(refresh_token);
                if (claimsPrincipal == null) return Unauthorized();
                var sesion = await sesionRepository.GetByHashAsync(refresh_token.ToSHA256String());
                if (sesion == null) return Unauthorized();
                var user = sesion.Usuario;
                if (!int.TryParse(configuration["Session:AccessTTL"], out int access_ttl)) access_ttl = Constants.DefaultAccessTTL;
                var access_claims = new List<Claim> {
                    new(Claims.Id, user.Id.ToString()),
                    new(Claims.Username, user.Username),
                    new(Claims.Role, user.Rol)
                };
                var access_token = jwtService.CreateToken(access_claims, TimeSpan.FromMinutes(access_ttl));
                return Ok(new AuthPayload
                {
                    AccessToken = access_token,
                });
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {

            try
            {
                var refresh_token = Request.Cookies["refresh_token"]?.ToString();
                if (string.IsNullOrWhiteSpace(refresh_token)) return BadRequest();

                var principal = jwtService.ValidateToken(refresh_token);

                var sesion = await sesionRepository.GetByHashAsync(refresh_token.ToSHA256String());
                if (sesion == null) return NotFound();

                await sesionRepository.DeleteAsync(sesion);
                return NoContent();
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }
    }
}
