using System.Security.Claims;
using Application.Auth;
using Application.UnitOfWork;

namespace Application.Services;

internal class CustomerService(IUnitOfWork unitOfWork, IUserService userService) : ICustomerService
{
    public async Task<int?> GetCurrentCustomerIdAsync(ClaimsPrincipal user)
    {
        var userDetailsModel = await userService.GetCurrentUserAsync(user);

        if (userDetailsModel is null)
            return null;

        return await unitOfWork.CustomerRepository.GetCustomerIdAsync(userDetailsModel.Id);
    }

    public async Task<int?> GetCustomerCartIdAsync(int customerId) =>
        await unitOfWork.CustomerRepository.GetCustomerCartIdAsync(customerId);

    public async Task CreateCustomerCartAsync(ClaimsPrincipal user)
    {
        var customerId =
            await GetCurrentCustomerIdAsync(user)
            ?? throw new NullReferenceException("Customer Id is null");
        await unitOfWork.CartRepository.CreateCartAsync(customerId, true);
        await unitOfWork.SaveChangesAsync();

        var cartId = unitOfWork.CartRepository.GetPersistedCartId();
        await AddCartToCustomerAsync(customerId, cartId);
    }

    public async Task AddCartToCustomerAsync(int customerId, int cartId)
    {
        await unitOfWork.CustomerRepository.AddCartToCustomerAsync(customerId, cartId);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveCartFromCustomerAsync(int customerId)
    {
        var customer = unitOfWork.CustomerRepository.RemoveCartFromCustomerAsync(customerId);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<int?> GetCustomerCartIdAsync(ClaimsPrincipal user)
    {
        var customerId = await GetCurrentCustomerIdAsync(user);

        if (customerId is null)
            return null;

        return await unitOfWork.CustomerRepository.GetCustomerCartIdAsync(customerId.Value);
    }
}
