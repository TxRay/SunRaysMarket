using Application.Repositories;
using Application.UnitOfWork;
using Infrastructure.Data;
using Infrastructure.Data.PersistenceModels;

namespace Infrastructure.Repositories;

internal class CustomerRepository(ApplicationDbContext dbContext) : ICustomerRepository
{
    public async Task CreateCustomerAsync(int userId)
    {
        var customer = new Customer
        {
            UserId = userId
        };
        
        await dbContext.Customers.AddAsync(customer);
    }

    public Task DeleteCustomerAsync(int customerId)
    {
        throw new NotImplementedException();
    }
}
