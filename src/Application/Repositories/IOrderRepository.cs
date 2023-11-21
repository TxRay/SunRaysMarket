using Application.DomainModels;

namespace Application.Repositories;

public interface IOrderRepository
{
    Task<OrderDetailsModel?> GetOrderDetailsAsync(int orderId);
    Task<IEnumerable<OrderLineListModel>> GetOrderLinesAsync(int orderId);
    Task<bool> OrderExistsAsync(int orderId);
    Task<bool> CreateOrderAsync(int cartId, int customerId);
    Task<bool> DeleteOrderAsync(int orderId);
}
