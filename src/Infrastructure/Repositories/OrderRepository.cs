using Application.DomainModels;
using Application.Repositories;

namespace Infrastructure.Repositories;

internal class OrderRepository : IOrderRepository
{
    public Task<OrderDetailsModel?> GetOrderDetailsAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderLineListModel>> GetOrderLinesAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> OrderExistsAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateOrderAsync(int cartId, int customerId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteOrderAsync(int orderId)
    {
        throw new NotImplementedException();
    }
}
