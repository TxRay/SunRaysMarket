namespace WebBlazor.Middleware;

public static class MiddlewareAppExtensions
{
    public static IApplicationBuilder UseShoppingCart(this IApplicationBuilder app)
    {
        app.UseMiddleware<ShoppingCartMiddleware>();

        return app;
    }
}
