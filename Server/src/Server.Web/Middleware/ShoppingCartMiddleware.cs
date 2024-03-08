namespace SunRaysMarket.Server.Web.Middleware;

public class ShoppingCartMiddleware(IServiceProvider serviceProvider, ICookieService cookieService)
    : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using var scope = serviceProvider.CreateScope();
        var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

        var currentCartId = cookieService.CartId;

        if (context.User.IsAuthenticated())
        {
            var sessionCartId = context.Session.GetInt32("customerCartId");
            var customerCartId =
                sessionCartId ?? await customerService.GetCustomerCartIdAsync(context.User);

            if (sessionCartId is null && customerCartId is not null)
                context.Session.SetInt32("customerCartId", customerCartId.Value);

            if (currentCartId is null && customerCartId is not null)
                cookieService.CartId = customerCartId.Value;
            else if (currentCartId is not null && customerCartId is null)
                await customerService.SaveCartAsync(context.User, currentCartId.Value);
        }

        await next.Invoke(context);
    }
}
