using System.Security.Claims;

namespace SunRaysMarket.Shared.Core.Utilities;

public static class ClaimsPrincipalExtensions
{
    public static bool IsAuthenticated(this ClaimsPrincipal user)
    {
        return user.Identity?.IsAuthenticated == true;
    }
}
