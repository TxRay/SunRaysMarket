using System.Text;
using System.Text.Json;
using SunRaysMarket.Server.Application.State;
using SunRaysMarket.Server.Core.Services;
using SunRaysMarket.Server.Core.Services.Auth;

namespace SunRaysMarket.Server.Web.Middleware;

/// <summary>
///     Middleware to manage the session state of the application.
/// </summary>
public class SessionStateMiddleware(IServiceScopeFactory serviceScopeFactory) : IMiddleware
{
    private const string ReturnUrlKey = "ReturnUrl";

    /// <inheritdoc />
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.GetEndpoint() is RouteEndpoint endpoint
            && endpoint.Metadata.Any(
                item => item is EndpointGroupNameAttribute
                        {
                            EndpointGroupName: "BlazorPageEndpoints"
                        }
                        && context.Request.Path.Value is { } path
                        && !path.Contains("_framework")
            )
           )
        {
            var sessionState = await PopulateSessionState(context);
            var sessionStateJson = JsonSerializer.Serialize(sessionState);

            context.Session.Set("SessionState", Encoding.UTF8.GetBytes(sessionStateJson));

            await next.Invoke(context);

            if (context.Request.Path.ToUriComponent() is { } path)
                context.Session.Set(ReturnUrlKey, Encoding.UTF8.GetBytes(path));
            else
                context.Session.Remove(ReturnUrlKey);
        }
        else
        {
            await next.Invoke(context);
        }
    }

    private async Task<SessionState> PopulateSessionState(HttpContext context)
    {
        await using var serviceScope = serviceScopeFactory.CreateAsyncScope();

        var path = context.Session.TryGetValue(ReturnUrlKey, out var pathByteArray)
            ? Encoding.UTF8.GetString(pathByteArray)
            : null;

        if (!context.User.IsAuthenticated()) return new SessionState { PageReturnUrl = path };

        var userService = serviceScope.ServiceProvider.GetRequiredService<IUserService>();
        var customerService = serviceScope.ServiceProvider.GetRequiredService<ICustomerService>();

        var userId = (await userService.GetCurrentUserAsync(context.User))?.Id;
        var customerId = await customerService.GetCurrentCustomerIdAsync(context.User);
        var cartId = await customerService.GetCustomerCartIdAsync(context.User);

        return new SessionState
        {
            AuthorizedUserId = userId,
            AuthorizedCustomerId = customerId,
            ActiveCartId = cartId,
            PageReturnUrl = path
        };
    }
}