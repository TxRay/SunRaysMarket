namespace SunRaysMarket.Server.Web.Middleware;

public static class MiddlewareAppExtensions
{
    public static IApplicationBuilder UseShoppingCart(this IApplicationBuilder app)
    {
        app.UseMiddleware<ShoppingCartMiddleware>();
        return app;
    }

    public static IApplicationBuilder UseCustomerPreferences(this IApplicationBuilder app)
    {
        app.UseMiddleware<CustomerPreferencesMiddleware>();
        return app;
    }

    public static IApplicationBuilder UseTrackedCookies(this IApplicationBuilder app)
    {
        app.UseMiddleware<TrackedCookiesMiddleware>();
        return app;
    }

    public static IApplicationBuilder UseSessionState(this IApplicationBuilder app)
    {
        app.UseMiddleware<SessionStateMiddleware>();
        return app;
    }

    public static IApplicationBuilder UseStatusCodeRedirect(this IApplicationBuilder app)
    {
        app.UseMiddleware<StatusCodeRedirectMiddleware>();
        return app;
    }
}