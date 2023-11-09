using System.Security.Claims;

namespace NodinSoftProjectAPI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst("Email")?.Value;
        }
    }
}
