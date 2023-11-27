using Application.DomainModels;

namespace Application.Repositories;

public interface ICustomerRepository
{
    Task CreateCustomerAsync(int userId);
    Task DeleteCustomerAsync(int customerId);
}
