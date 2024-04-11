using Microsoft.Extensions.Logging;
using SunRaysMarket.Server.Application.Checkout.Results;
using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Shared.Core.Utilities.OrderCalculations;

namespace SunRaysMarket.Server.Application.Checkout.CheckoutHandlers;

public class UpdateOrderAmountHandler : ICheckoutHandler
{
    private readonly ILogger<UpdateOrderAmountHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderAmountHandler(
        ILogger<UpdateOrderAmountHandler> logger,
        IUnitOfWork unitOfWork
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<CheckoutHandlerResponse> HandleAsync(CheckoutContext context)
    {
        if (!context.HandlerResults.TryGetValue<CreateOrderResult>(out var createOrderResult))
            return new CheckoutHandlerResponse.Error("No order was found.");

        try
        {
            await _unitOfWork
                .OrderRepository
                .UpdateOrderAmountAsync(
                    createOrderResult!.OrderId,
                    (orderSummary, orderItemAmounts) =>
                        orderSummary.CalculateAmounts(orderItemAmounts)
                );

            await _unitOfWork.SaveChangesAsync();

            var persistedOrder = await _unitOfWork
                .OrderRepository
                .GetOrderDetailsAsync(createOrderResult.OrderId);

            return persistedOrder is not null
                ? new CheckoutHandlerResponse.Result<UpdateOrderAmountResult>(
                    new UpdateOrderAmountResult(persistedOrder.Total)
                )
                : new CheckoutHandlerResponse.Error("No order was found.");
        }
        catch (Exception e)
        {
            _logger.LogError("{}", e.Message);

            return new CheckoutHandlerResponse.Error(
                "Something went wrong while trying to update the order amount."
            );
        }
    }
}
