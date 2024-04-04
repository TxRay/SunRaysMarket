using SunRaysMarket.Shared.Core.DomainModels.Responses;

namespace SunRaysMarket.Server.Web.Endpoints;

internal static class StoreLocationEndpoints
{
    public static IEndpointRouteBuilder MapStoreLocationEndpoints(
        this IEndpointRouteBuilder endpoints
    )
    {
        var storeLocationsGroup = endpoints.MapGroup("/store-locations");

        storeLocationsGroup.MapGet("/", GetAllStoreLocationsHandler);
        storeLocationsGroup.MapGet("/preferred", GetPreferredStoreLocationHandler);

        return endpoints;
    }

    private static async Task<IResult> GetAllStoreLocationsHandler(
        IStoreLocationService storeLocationService
    )
    {
        return Results.Json(
            new StoreLocationsResponse
            {
                StoreLocations = await storeLocationService.GetStoreLocationsAsync()
            }
        );
    }

    private static async Task<IResult> GetPreferredStoreLocationHandler(IStoreLocationService storeLocationService)
        => Results.Json(
            new CustomerStorePreferenceResponse
            {
                PreferredStoreId = await storeLocationService.GetPreferredStoreAsync()
            }
        );
}
