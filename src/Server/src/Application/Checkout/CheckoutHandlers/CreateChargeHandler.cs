using Microsoft.Extensions.Logging;
using SunRaysMarket.Server.Application.Checkout.Results;
using SunRaysMarket.Server.Core.DomainModels.Payment;
using SunRaysMarket.Server.Core.Services;
using SunRaysMarket.Shared.Core.Services;

namespace SunRaysMarket.Server.Application.Checkout.CheckoutHandlers;

public class CreateChargeHandler : ICheckoutHandler
{
    private readonly ICustomerService _customerService;
    private readonly ILogger<CreateChargeHandler> _logger;
    private readonly IPaymentService _paymentService;

    public CreateChargeHandler(
        ICustomerService customerService,
        ILogger<CreateChargeHandler> logger,
        IPaymentService paymentService
    )
    {
        _customerService = customerService;
        _logger = logger;
        _paymentService = paymentService;
    }

    public async Task<CheckoutHandlerResponse> HandleAsync(CheckoutContext context)
    {
        var customerPaymentId = await _customerService.GetCustomerPaymentIdAsync(
            context.HttpContext.User
        );

        if (customerPaymentId is null)
        {
            const string message =
                "The customer's account does not have payment processing set up.";
            _logger.LogError("{}", message);
        }

        if (!context.HandlerResults.TryGetValue<CreateOrderResult>(out var createOrderResult))
            return new CheckoutHandlerResponse.Error("No order was found.");

        if (
            !context
                .HandlerResults
                .TryGetValue<UpdateOrderAmountResult>(out var updateOrderAmountResult)
        )
            return new CheckoutHandlerResponse.Error(
                "The total order amount cannot be determined."
            );

        var chargeInfo = new CreateChargeModel
        {
            Amount = (long)(100 * updateOrderAmountResult!.Amount),
            Currency = "usd",
            CustomerPaymentId = customerPaymentId,
            Source = context.SubmitModel.PaymentMethodId
        };

        try
        {
            var chargeResponse = await _paymentService.CreateCharge(chargeInfo);

            return chargeResponse is not null
                ? new CheckoutHandlerResponse.Result<CreateChargeResult>(
                    new CreateChargeResult(chargeResponse.Id)
                )
                : new CheckoutHandlerResponse.Error("The payment could not be processed.");
        }
        catch (Exception e)
        {
            _logger.LogError("{}", e.Message);
            return new CheckoutHandlerResponse.Error(
                "Something went wrong while trying to process the payment."
            );
        }
    }
}
