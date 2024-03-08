namespace SunRaysMarket.Server.Web.Middleware;

public class TrackedCookiesMiddleware(ICookieService cookieService) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next.Invoke(context);
        cookieService.Reset();
    }
}
