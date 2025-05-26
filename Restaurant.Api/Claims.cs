using System.Security.Claims;

namespace Restaurant.Api
{
    public static class Claims
    {
        public const string Id = "id";
        public const string Username = ClaimTypes.Name;
        public const string Role = ClaimTypes.Role;
    }
}
