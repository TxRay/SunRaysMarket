using Application.Services;
using Application.Utilities;
using Web.Cookies;

namespace WebBlazor.Middleware;

public class ShoppingCartMiddleware(IServiceProvider serviceProvider) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using var scope = serviceProvider.CreateScope();
        var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

        var currentCartId = context.Request.Cookies.GetCartIdCookie();

        if (context.User.IsAuthenticated())
        {
            var customerCartId = await customerService.GetCustomerCartIdAsync(context.User);

            if (currentCartId is null && customerCartId is not null)
                context.Response.Cookies.SetCartIdCookie(customerCartId.Value);
            else if (currentCartId is not null && customerCartId is null)
                await customerService.SaveCartAsync(context.User, currentCartId.Value);
        }

        await next.Invoke(context);
    }
}
