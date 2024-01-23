using System.Security.Claims;

namespace SunRaysMarket.Shared.Core.Utilities;

public static class ClaimsPrincipalExtensions
{
    public static bool IsAuthenticated(this ClaimsPrincipal user) =>
        user.Identity?.IsAuthenticated == true;
}
