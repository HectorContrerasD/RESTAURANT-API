using System.Security.Claims;

namespace Restaurant.Api.Services.Abstractions
{
    public interface IJwtService
    {
        public string CreateToken(IEnumerable<Claim> claims, TimeSpan ttl);
        public ClaimsPrincipal? ValidateToken(string token);
    }
}
