using Microsoft.AspNetCore.Mvc;
using SunRaysMarket.Server.Core.DomainModels.Payment;
using SunRaysMarket.Server.Core.Services;

namespace SunRaysMarket.Server.Web.Endpoints;

internal static class PaymentEndpoints
{
    public static IEndpointRouteBuilder MapPaymentEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var paymentGroup = endpoints
            .MapGroup("/payments")
            .WithGroupName("Payment")
            .WithDescription("Endpoints for managing payments.");

        paymentGroup.MapPost(
            "/charge",
            async ([FromBody] CreateChargeModel paymentModel, IPaymentService paymentService) =>
            {
                var chargeResponse = await paymentService.CreateCharge(paymentModel);

                return Results.Json(chargeResponse);
            }
        );

        paymentGroup.MapPost(
            "/customer",
            async (
                [FromBody] CreatePaymentCustomerModel customerModel,
                IPaymentService paymentService
            ) =>
            {
                var customerResponse = await paymentService.CreateCustomer(customerModel);

                return Results.Json(customerResponse);
            }
        );

        return endpoints;
    }
}