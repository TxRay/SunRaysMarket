using Microsoft.AspNetCore.Mvc;
using SunRaysMarket.Shared.Core.DomainModels.Checkout;
using SunRaysMarket.Shared.Core.DomainModels.Responses;

namespace SunRaysMarket.Server.Web.Endpoints;

public static class CheckoutEndpoints
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
                    new StoreLocationsResponse(await checkoutService.GetStoreLocationsAsync())
                )
        );

        checkoutGroup.MapPost(
            "/",
            (
                [FromBody] CheckoutSubmitModel checkoutSubmission,
                HttpContext context,
                ICheckoutService checkoutService
            ) => checkoutService.CheckoutAsync(checkoutSubmission)
        );

        return endpoints;
    }
}
