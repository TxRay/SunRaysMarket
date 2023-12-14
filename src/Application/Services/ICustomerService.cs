using System.Security.Claims;

namespace Application.Services;

public interface ICustomerService
{
    Task<int?> GetCurrentCustomerIdAsync(ClaimsPrincipal user);

    Task<int?> GetCustomerCartIdAsync(int customerId);

    Task CreateCustomerCartAsync(ClaimsPrincipal user);
    Task SaveCartAsync(ClaimsPrincipal user, int cartId);

    Task RemoveCartFromCustomerAsync(int customerId);

    Task<string?> GetCustomerPaymentIdAsync(ClaimsPrincipal user);

    Task<int?> GetCustomerCartIdAsync(ClaimsPrincipal user);
}
