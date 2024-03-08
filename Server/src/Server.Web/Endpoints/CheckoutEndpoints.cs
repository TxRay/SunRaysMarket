using Microsoft.AspNetCore.Mvc;
using SunRaysMarket.Shared.Core.DomainModels.Checkout;
using SunRaysMarket.Shared.Core.DomainModels.Responses;

namespace SunRaysMarket.Server.Web.Endpoints;

internal static class CheckoutEndpoints
{
    public static IEndpointRouteBuilder MapCheckoutEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var checkoutGroup = endpoints
            .MapGroup("/checkout")
            .WithGroupName("Checkout")
            .WithDescription("Endpoints for the checkout Wizard.");

        checkoutGroup.MapGet(
            "/timeslots/{storeId}/{orderType}",
            async (int storeId, OrderType orderType, ICheckoutService checkoutService) =>
            {
                var timeSlots = await checkoutService.GetCheckoutTimeSlotsAsync(storeId, orderType);
                return Results.Json(timeSlots);
            }
        );

        checkoutGroup.MapGet(
            "/timeslot/{checkoutId:int}",
            async (int checkoutId, ICheckoutService checkoutService) =>
                Results.Json(await checkoutService.GetCheckoutTimeSlotAsync(checkoutId))
        );

        checkoutGroup.MapGet(
            "/locations",
            async (ICheckoutService checkoutService) =>
                Results.Json(
                    new StoreLocationsResponse
                    {
                        StoreLocations = await checkoutService.GetStoreLocationsAsync()
                    }
                )
        );

        checkoutGroup.MapPost("/", CheckoutHandler);

        return endpoints;
    }

    private static async Task<IResult> CheckoutHandler(
        [FromBody] CheckoutSubmitModel checkoutSubmission,
        ICheckoutService checkoutService
    )
    {
        var response = await checkoutService.CheckoutAsync(checkoutSubmission);
        return Results.Json(response);
    }
}
