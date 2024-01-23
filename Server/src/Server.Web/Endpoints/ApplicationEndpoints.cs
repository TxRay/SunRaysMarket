//using Web.Models.EndpointModels;

namespace SunRaysMarket.Server.Web.Endpoints;




/*public static class ApplicationEndpoints
{
    public static IEndpointRouteBuilder MapApplicationEndpoints(
        this IEndpointRouteBuilder endpoints
    )
    {
        var applicationGroup = endpoints
            .MapGroup("/app")
            .WithGroupName("Application")
            .WithDescription("Endpoints for managing applications.");
        
        applicationGroup.MapSessionEndpoints();

        return endpoints;
    }

    private static IEndpointRouteBuilder MapSessionEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var sessionGroup = endpoints
            .MapGroup("/session")
            .WithGroupName("Session")
            .WithDescription("Endpoints for managing sessions.");

        sessionGroup.MapPost(
            "/login-email",
            (LoginFormSessionEndpointModel model, HttpContext context) =>
            {
                var session = context.Session;
                
                if (!session.IsAvailable)
                {
                    return Results.BadRequest();
                }
                
                session.SetString("Email", model.Email);

                return Results.Ok();
            }
        );

        return endpoints;
    }
}*/
