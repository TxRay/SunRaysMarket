using System.Security.Claims;
using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Server.Core.DomainModels;
using SunRaysMarket.Server.Core.Services;
using SunRaysMarket.Shared.Core.Utilities.OrderCalculations;

namespace SunRaysMarket.Server.Application.ServicesImpl.Scoped;

public class OrderService(ICustomerService customerService, IUnitOfWork unitOfWork) : IOrderService
{
    public async Task<(int?, float?)> CreateOrderAsync(
        ClaimsPrincipal user,
        int timeSlotId,
        OrderType orderType,
        int? deliveryAddressId
    )
    {
        var customerId = await customerService.GetCurrentCustomerIdAsync(user);

        if (customerId is null)
            return (null, null);

        var newOrder = new CreateOrderModel
        {
            CustomerId = customerId.Value,
            DeliveryAddressId = deliveryAddressId,
            StoreId = 2,
            TimeSlotId = timeSlotId,
            OrderType = orderType
        };

        await unitOfWork.OrderRepository.CreateOrderAsync(newOrder);
        await unitOfWork.SaveChangesAsync();

        var persistedOrderId = unitOfWork.OrderRepository.GetPersistedOrderId();

        if (persistedOrderId is null)
            return (null, null);

        await CreateInitialOrderLinesAsync(persistedOrderId.Value, user);
        await unitOfWork
            .OrderRepository
            .UpdateOrderAmountAsync(
                persistedOrderId.Value,
                (orderSummary, orderItemAmounts) => orderSummary.CalculateAmounts(orderItemAmounts)
            );
        await unitOfWork.SaveChangesAsync();
        var persistedOrder = await unitOfWork
            .OrderRepository
            .GetOrderDetailsAsync(persistedOrderId.Value);

        return persistedOrder is not null
            ? (persistedOrder.Id, persistedOrder.Total)
            : (null, null);
    }

    public async Task CreateInitialOrderLinesAsync(int orderId, ClaimsPrincipal user)
    {
        var cartId = await customerService.GetCustomerCartIdAsync(user);

        if (cartId is null)
            return;

        var cartItems = await unitOfWork.CartRepository.GetCartItemsAsync(cartId.Value);
        var orderLines = cartItems.Select(
            ci =>
                new CreateOrderLineModel
                {
                    OrderId = orderId,
                    ItemId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.RegularPrice,
                    Discount = ci.Discount,
                    TotalPrice = ci.ProductPrice
                }
        );

        await unitOfWork.OrderRepository.AddOrderLinesAsync(orderLines);
        await unitOfWork.SaveChangesAsync();
    }
}