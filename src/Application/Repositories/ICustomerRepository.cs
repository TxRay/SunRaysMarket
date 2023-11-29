using Application.DomainModels;

namespace Application.Repositories;

public interface ICustomerRepository
{
    Task<int?> GetCustomerIdAsync(int userId);
    Task CreateCustomerAsync(int userId);
    Task AddCartToCustomerAsync(int customerId, int cartId);
    
    Task RemoveCartFromCustomerAsync(int customerId);
    Task DeleteCustomerAsync(int customerId);
    
    Task<int?> GetCustomerCartIdAsync(int customerId);
}
