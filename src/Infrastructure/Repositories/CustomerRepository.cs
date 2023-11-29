using Application.Repositories;
using Application.UnitOfWork;
using Infrastructure.Data;
using Infrastructure.Data.PersistenceModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class CustomerRepository(ApplicationDbContext dbContext) : ICustomerRepository
{
    public async Task<int?> GetCustomerIdAsync(int userId)
    => await dbContext.Customers
        .Include(customer => customer.User )
        .Where(customer => customer.UserId == userId)
        .Select(customer => customer.Id)
        .FirstOrDefaultAsync();

    public async Task CreateCustomerAsync(int userId)
    {
        var customer = new Customer
        {
            UserId = userId
        };
        
        await dbContext.Customers.AddAsync(customer);
    }

    public async Task AddCartToCustomerAsync(int customerId, int cartId)
    {
        var customer = await dbContext.Customers.FindAsync(customerId);
        
        if(customer is not null)
            customer.CartId = cartId;
    }

    public async Task RemoveCartFromCustomerAsync(int customerId)
    {
        var customer = await dbContext.Customers.FindAsync(customerId);
        
        if(customer is not null)
            customer.CartId = null;
    }

    public Task DeleteCustomerAsync(int customerId)
    {
        throw new NotImplementedException();
    }

    public async Task<int?> GetCustomerCartIdAsync(int customerId)
    => await dbContext.Customers
        .Include(customer => customer.Cart)
        .Where(customer => customer.Id == customerId)
        .Select(customer => customer.CartId)
        .FirstOrDefaultAsync();
}
