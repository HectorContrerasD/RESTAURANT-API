using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurant.Api.Services
{
    public class JwtService(IConfiguration configuration)
    {
        public string CreateToken(IEnumerable<Claim>claims, TimeSpan ttl)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var secret = configuration["Jwt:Secret"] ?? throw new ApplicationException("'Jwt:Secret' is mandatory settings value");

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.UtcNow.Add(ttl),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)), SecurityAlgorithms.HmacSha256)
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateEncodedJwt(descriptor);
            return token;
        }
        public ClaimsPrincipal? ValidateToken(string token)
        {
            try
            {
                var issuer = configuration["Jwt:Issuer"];
                var audience = configuration["Jwt:Audience"];
                var secret = configuration["Jwt:Secret"] ?? throw new ApplicationException("'Jwt:Secret' is mandatory settings value");

                var validationParameters = new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true
                };

                var handler = new JwtSecurityTokenHandler();
                var principal = handler.ValidateToken(token, validationParameters, out SecurityToken _);
                return principal;
            }
            catch (Exception error)
            {
                Console.WriteLine("Token validation failed: " + error.Message);
                return null;
            }
        }
    }
}
