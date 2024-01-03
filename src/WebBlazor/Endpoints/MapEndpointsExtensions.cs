namespace WebBlazor.Endpoints;

public static class MapEndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapComponentEndpoints();
        
        var apiGroup = endpoints
            .MapGroup("/api")
            .WithGroupName("Api")
            .WithDescription("Api endpoints");

        apiGroup.MapImageEndpoints();
        //apiGroup.MapApplicationEndpoints();
        apiGroup.MapAddressEndpoints();
        apiGroup.MapCartEndpoints();
        apiGroup.MapCheckoutEndpoints();
        apiGroup.MapPaymentEndpoints();
        apiGroup.MapProductEndpoints();

        return endpoints;
    }
}
