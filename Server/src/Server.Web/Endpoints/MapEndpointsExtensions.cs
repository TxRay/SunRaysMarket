namespace SunRaysMarket.Server.Web.Endpoints;

public static class MapEndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder endpoints)
    {
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
        apiGroup.MapCustomerEndpoints();
        apiGroup.MapStoreLocationEndpoints();

        return endpoints;
    }
}
