using Application.DomainModels.Payment;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebBlazor.Endpoints;

public static class PaymentEndpoints
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
