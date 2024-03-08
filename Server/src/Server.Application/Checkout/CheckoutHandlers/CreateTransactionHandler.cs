using Microsoft.Extensions.Logging;
using SunRaysMarket.Server.Application.Checkout.Results;
using SunRaysMarket.Server.Application.Services;

namespace SunRaysMarket.Server.Application.Checkout.CheckoutHandlers;

public class CreateTransactionHandler : ICheckoutHandler
{
    private readonly ILogger<CreateTransactionHandler> _logger;
    private readonly ITransactionService _transactionService;

    public CreateTransactionHandler(
        ILogger<CreateTransactionHandler> logger,
        ITransactionService transactionService
    )
    {
        _logger = logger;
        _transactionService = transactionService;
    }

    public async Task<CheckoutHandlerResponse> HandleAsync(CheckoutContext context)
    {
        if (!context.HandlerResults.TryGetValue<CreateOrderResult>(out var createOrderResult))
            return new CheckoutHandlerResponse.Error(
                "Could not create a transaction because no order number was found."
            );

        if (!context.HandlerResults.TryGetValue<CreateChargeResult>(out var chargeResult))
            return new CheckoutHandlerResponse.Error(
                "Could not create a transaction because the customer's account was never charged."
            );

        if (
            !context
                .HandlerResults
                .TryGetValue<UpdateOrderAmountResult>(out var updateOrderAmountResult)
        )
            return new CheckoutHandlerResponse.Error(
                "Could not create a transaction because the total order amount could not be found."
            );

        try
        {
            await _transactionService.CreateTransactionAsync(
                createOrderResult!.OrderId,
                updateOrderAmountResult!.Amount,
                chargeResult!.ChargeId
            );
        }
        catch (Exception e)
        {
            _logger.LogError("{}", e.Message);
            return new CheckoutHandlerResponse.Error(
                "Something went wrong while trying to create a transaction."
            );
        }

        return new CheckoutHandlerResponse.Empty();
    }
}
