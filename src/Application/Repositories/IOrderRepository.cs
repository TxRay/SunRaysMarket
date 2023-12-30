using Application.DomainModels;
using Application.Utilities.OrderCalculations;

namespace Application.Repositories;

public interface IOrderRepository
{
    Task<OrderDetailsModel?> GetOrderDetailsAsync(int orderId);
    Task<OrderDetailsModel?> GetOrderDetailsAsync(long orderNumber);
    
    Task<IEnumerable<OrderListModel>> GetOrdersAsync();
    Task<IEnumerable<OrderListModel>> GetOrdersAsync(int customerId);
    Task<IEnumerable<OrderLineListModel>> GetOrderLinesAsync(int orderId);
    Task<bool> OrderExistsAsync(int orderId);
    Task<bool> CreateOrderAsync(CreateOrderModel model);
    Task AddOrderLineAsync(CreateOrderLineModel model);

    Task AddOrderLinesAsync(IEnumerable<CreateOrderLineModel> models);

    Task UpdateOrderAmountAsync(
        int orderId,
        Action<IOrderPriceSummary, IEnumerable<IOrderItemAmounts>> calculateAmounts
    );
    Task DeleteOrderAsync(int orderId);

    Task DeleteOrderLineAsync(int orderLineId);

    int? GetPersistedOrderId();

    OrderDetailsModel? GetPersistedOrderDetails();
}
