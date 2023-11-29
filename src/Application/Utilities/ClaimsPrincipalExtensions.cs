using System.Security.Claims;

namespace Application.Utilities;

public static class ClaimsPrincipalExtensions
{
    public static bool IsAuthenticated(this ClaimsPrincipal user) =>
        user.Identity?.IsAuthenticated == true;
}
