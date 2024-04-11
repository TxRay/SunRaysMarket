namespace SunRaysMarket.Server.Web.Middleware;

public class StatusCodeRedirectMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next.Invoke(context);

        var path = context.Request.Path.ToUriComponent(); 
        
        if (context.Response.StatusCode >= 400
            && !path.Contains("_framework")
            && !path.Contains("_blazor")
            && !path.StartsWith("/api/")
           )
        {
            var redirectRoute = context.Response.StatusCode switch
            {
                404 => "/error/404/not-found",
                500 => "error/500/internal-server-error",
                _ => $"error/{context.Response.StatusCode}"
            };

            context.Response.Redirect(redirectRoute);
        }
    }
}