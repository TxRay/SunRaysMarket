using Application.DomainModels;

namespace Application.Repositories;

public interface ICustomerRepository
{
    Task<int> CreateCustomerAsync(int userId);
    Task DeleteCustomerAsync(int customerId);
}
