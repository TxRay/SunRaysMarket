using Application.Repositories;

namespace Infrastructure.Repositories;

internal class CustomerRepository : ICustomerRepository
{
    public Task<int> CreateCustomerAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCustomerAsync(int customerId)
    {
        throw new NotImplementedException();
    }
}
