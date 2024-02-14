namespace SunRaysMarket.Server.Web.Middleware;

public static class RegisterMiddleware
{
    public static IServiceCollection AddCustomMiddleware(this IServiceCollection services)
    {
        services.AddSingleton<ShoppingCartMiddleware>();
        services.AddSingleton<CustomerPreferencesMiddleware>();
        services.AddSingleton<TrackedCookiesMiddleware>();

        return services;
    }
}
