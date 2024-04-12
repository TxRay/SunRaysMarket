using Microsoft.EntityFrameworkCore.ChangeTracking;
using SunRaysMarket.Server.Application.Repositories;
using SunRaysMarket.Server.Core.DomainModels;

namespace SunRaysMarket.Server.Infrastructure.Repositories;

internal class CustomerRepository(ApplicationDbContext dbContext) : ICustomerRepository
{
    private EntityEntry<Customer>? CustomerEntry { get; set; }

    public async Task<int?> GetCustomerIdAsync(int userId)
    {
        return await dbContext
            .Customers
            .Include(customer => customer.User)
            .Where(customer => customer.UserId == userId)
            .Select(customer => customer.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<CustomerDetailsModel?> GetCustomerDetailsAsync(int customerId)
    {
        return await dbContext
            .Customers
            .Include(c => c.User)
            .Where(c => c.Id == customerId)
            .Select(
                c =>
                    new CustomerDetailsModel
                    {
                        Id = c.Id,
                        Email = c.User.Email,
                        FirstName = c.User.FirstName,
                        LastName = c.User.LastName
                    }
            )
            .FirstOrDefaultAsync();
    }

    public async Task<bool> CreateCustomerAsync(int userId)
    {
        var customer = new Customer { UserId = userId };

        CustomerEntry = await dbContext.Customers.AddAsync(customer);

        return CustomerEntry.State == EntityState.Added;
    }

    public async Task<bool> AddPaymentIdAsync(int customerId, string paymentId)
    {
        var customer = await dbContext.Customers.FindAsync(customerId);

        if (customer is null)
            return false;

        customer.PaymentId = paymentId;

        var customerEntry = dbContext.Customers.Update(customer);

        return customerEntry.State == EntityState.Modified;
    }

    public async Task AddCartToCustomerAsync(int customerId, int cartId)
    {
        var customer = await dbContext.Customers.FindAsync(customerId);

        if (customer is not null)
            customer.CartId = cartId;
    }

    public async Task RemoveCartFromCustomerAsync(int customerId)
    {
        var customer = await dbContext.Customers.FindAsync(customerId);

        if (customer is not null)
            customer.CartId = null;
    }

    public Task DeleteCustomerAsync(int customerId)
    {
        throw new NotImplementedException();
    }

    public async Task<int?> GetCustomerCartIdAsync(int customerId)
    {
        return await dbContext
            .Customers
            .Include(customer => customer.Cart)
            .Where(customer => customer.Id == customerId)
            .Select(customer => customer.CartId)
            .FirstOrDefaultAsync();
    }

    public async Task<string?> GetCustomerPaymentIdAsync(int customerId)
    {
        return await dbContext
            .Customers
            .Where(customer => customer.Id == customerId)
            .Select(customer => customer.PaymentId)
            .FirstOrDefaultAsync();
    }

    public async Task AddCustomerAddressAsync(int customerId, int addressId)
    {
        var customerAddress = new CustomerAddress
        {
            CustomerId = customerId,
            AddressId = addressId
        };

        await dbContext.CustomerAddresses.AddAsync(customerAddress);
    }

    public async Task RemoveCustomerAddressAsync(int customerId, int addressId)
    {
        var customerAddress = await dbContext
            .CustomerAddresses
            .FirstOrDefaultAsync(ca => ca.CustomerId == customerId && ca.AddressId == addressId);

        if (customerAddress is not null)
            dbContext.Remove(customerAddress);
    }

    public async Task<IEnumerable<AddressModel>> GetCustomerAddresses(int customerId)
    {
        return await dbContext
            .CustomerAddresses
            .Include(ca => ca.Address)
            .Where(ca => ca.CustomerId == customerId)
            .Select(
                ca =>
                    new AddressModel
                    {
                        Id = ca.Address.Id,
                        Street = ca.Address.Street,
                        City = ca.Address.City,
                        PostalCode = ca.Address.PostalCode,
                        Country = ca.Address.Country
                    }
            )
            .ToArrayAsync();
    }

    public async Task SetCustomerPreferences(int customerId, UpdateCustomerPreferencesModel model)
    {
        var entity = await dbContext.Customers.FindAsync(customerId);

        if (entity is null)
            return;

        foreach (var propertyInfo in model.GetType().GetProperties())
        {
            var entityPropertyInfo =
                entity.GetType().GetProperty(propertyInfo.Name)
                ?? throw new InvalidOperationException(
                    $"$The entity {typeof(Customer).FullName} does not contain "
                    + $"a property named '{propertyInfo}'."
                );
            object? value;
            if ((value = propertyInfo.GetValue(model)) != entityPropertyInfo.GetValue(entity))
                entityPropertyInfo.SetValue(entity, value);
        }
    }

    public Task<CustomerPreferences?> GetCustomerPreferences(int customerId)
    {
        return dbContext
            .Customers
            .Where(c => c.Id == customerId)
            .Select(c => new CustomerPreferences { PreferredStoreId = c.PreferredStoreId })
            .FirstOrDefaultAsync();
    }

    public Task<object?> GetCustomerPreferences(
        int customerId,
        Func<CustomerPreferences, object?> selector
    )
    {
        return dbContext
            .Customers
            .Where(c => c.Id == customerId)
            .Select(
                c => selector(new CustomerPreferences { PreferredStoreId = c.PreferredStoreId })
            )
            .FirstOrDefaultAsync();
    }

    public int? GetPersistedCustomerId()
    {
        return CustomerEntry is not null && CustomerEntry.State == EntityState.Unchanged
            ? CustomerEntry.Entity.Id
            : null;
    }
}