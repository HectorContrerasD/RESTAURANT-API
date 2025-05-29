using System.Security.Claims;

namespace Restaurant.Api.Services.Utils
{
    public static class ClaimsPrincipalExtension
    {
        public static string? GetRole(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.FirstOrDefault(x => x.Type == Claims.Role);
            return claim?.Value;
        }
        public static int? GetId(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.FirstOrDefault(x => x.Type == Claims.Id);
            if (claim != null && int.TryParse(claim.Value, out int id)) return id;
            return null;
        }
    }
}
