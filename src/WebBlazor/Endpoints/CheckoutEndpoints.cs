using Application.DomainModels.Checkout;
using Application.Enums;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebBlazor.Endpoints;

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
