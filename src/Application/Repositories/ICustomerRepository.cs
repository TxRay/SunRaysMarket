using Application.DomainModels;

namespace Application.Repositories;

public interface ICustomerRepository
{
    Task<int?> GetCustomerIdAsync(int userId);

    Task<CustomerDetailsModel?> GetCustomerDetailsAsync(int customerId);
    Task<bool> CreateCustomerAsync(int userId);

    Task<bool> AddPaymentIdAsync(int customerId, string paymentId);
    Task AddCartToCustomerAsync(int customerId, int cartId);

    Task RemoveCartFromCustomerAsync(int customerId);
    Task DeleteCustomerAsync(int customerId);

    Task<int?> GetCustomerCartIdAsync(int customerId);

    Task<string?> GetCustomerPaymentIdAsync(int customerId);

    int? GetPersistedCustomerId();
}
