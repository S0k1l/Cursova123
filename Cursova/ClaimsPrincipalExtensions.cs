using System.Security.Claims;

namespace Cursova
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            var claims = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            return claims;
        }
    }
}
