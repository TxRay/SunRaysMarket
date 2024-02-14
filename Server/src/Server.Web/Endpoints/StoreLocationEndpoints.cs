using SunRaysMarket.Shared.Core.DomainModels.Responses;

namespace SunRaysMarket.Server.Web.Endpoints;

internal static class StoreLocationEndpoints
{
    public static IEndpointRouteBuilder MapStoreLocationEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var storeLocationsGroup = endpoints.MapGroup("/store-locations");

        storeLocationsGroup.MapGet("/", GetAllStoreLocationsHandler);

        return endpoints;
    }

    private static async Task<IResult> GetAllStoreLocationsHandler(IStoreLocationService storeLocationService)
        => Results.Json(
            new StoreLocationsResponse
            {
                StoreLocations = await storeLocationService.GetStoreLocationsAsync()
            }
        );
}