using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Restaurant.Api.Payloads;
using Restaurant.Api.Repositories.Abstractions;
using Restaurant.Api.Repositories.Validators;
using Restaurant.Api.Services.Abstractions;
using Restaurant.Api.Services.Utils;
using System.Security.Claims;

namespace Restaurant.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController(IConfiguration configuration, IUserRepository userRepository, IJwtService jwtService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginPayload request)
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
            return Ok(new AuthPayload
            {
                AccessToken = access_token,
            });
        }
        //[HttpPost("logout")]
        //public async Task<IActionResult> Logout()
        //{
        //    Response.Cookies.Delete("refresh_token");
        //    return Ok();
        //}
    }
}
