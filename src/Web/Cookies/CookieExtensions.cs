namespace Web.Cookies;

public static class CookieExtensions
{
    private const string Prefix = ".SunRaysMarket.";
    private const string CartKey = Prefix + "Cart";
    
    public static void SetCartIdCookie(this IResponseCookies cookies, int cartId)
        => cookies.Append(CartKey, cartId.ToString(), new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(30)
        });
    
    public static int? GetCartIdCookie(this IRequestCookieCollection cookies) 
        => cookies.TryGetValue(CartKey, out var value) ? int.Parse(value) : null;
}