using System.Security.Claims;

namespace Application.Services;

public interface ICustomerService
{
    Task<int?> GetCurrentCustomerIdAsync(ClaimsPrincipal user);
    
    Task<int?> GetCustomerCartIdAsync(int customerId);
    
    Task CreateCustomerCartAsync(ClaimsPrincipal user);
    Task AddCartToCustomerAsync(int customerId, int cartId);
    
    Task RemoveCartFromCustomerAsync(int customerId);

    Task<int?> GetCustomerCartIdAsync(ClaimsPrincipal user);
}