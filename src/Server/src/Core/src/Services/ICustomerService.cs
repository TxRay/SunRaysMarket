using System.Security.Claims;

namespace SunRaysMarket.Server.Core.Services;

public interface ICustomerService
{
    Task<int?> GetCurrentCustomerIdAsync(ClaimsPrincipal user);
    Task<int?> GetCurrentCustomerIdAsync();
    Task<int?> GetCustomerCartIdAsync(int customerId);
    Task CreateCustomerCartAsync(ClaimsPrincipal user);
    Task SaveCartAsync(ClaimsPrincipal user, int cartId);
    Task RemoveCartFromCustomerAsync();
    Task<string?> GetCustomerPaymentIdAsync(ClaimsPrincipal user);

    Task<int?> GetCustomerCartIdAsync(ClaimsPrincipal user);
}