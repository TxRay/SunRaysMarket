using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SunRaysMarket.Server.Application.Services;
using SunRaysMarket.Server.Application.Services.Auth;
using SunRaysMarket.Server.Application.UnitOfWork;

namespace SunRaysMarket.Server.Application.ServicesImpl.Scoped;

internal class CustomerService(
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork,
    IUserService userService)
    : ICustomerService
{
    public async Task<int?> GetCurrentCustomerIdAsync(ClaimsPrincipal user)
    {
        var userDetailsModel = await userService.GetCurrentUserAsync(user);

        if (userDetailsModel is null)
            return null;

        return await unitOfWork.CustomerRepository.GetCustomerIdAsync(userDetailsModel.Id);
    }

    public async Task<int?> GetCurrentCustomerIdAsync()
        => httpContextAccessor.HttpContext?.User is not null
            ? await GetCurrentCustomerIdAsync(httpContextAccessor.HttpContext.User)
            : null;

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
        await SaveCartAsync(customerId, cartId);
    }

    private async Task SaveCartAsync(int customerId, int cartId)
    {
        await unitOfWork.CustomerRepository.AddCartToCustomerAsync(customerId, cartId);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task SaveCartAsync(ClaimsPrincipal user, int cartId)
    {
        var customerId =
            await GetCurrentCustomerIdAsync(user)
            ?? throw new NullReferenceException("Customer Id is null");

        await SaveCartAsync(customerId, cartId);
    }

    public async Task RemoveCartFromCustomerAsync()
    {
        var customerId = await GetCurrentCustomerIdAsync();
        
        if(customerId is null )
            return;
        
        await unitOfWork.CustomerRepository.RemoveCartFromCustomerAsync(customerId.Value);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveCartFromCustomerAsync(int customerId)
    {

    }

    public async Task<string?> GetCustomerPaymentIdAsync(ClaimsPrincipal user)
    {
        var customerId = await GetCurrentCustomerIdAsync(user);

        return customerId is null
            ? null
            : await unitOfWork.CustomerRepository.GetCustomerPaymentIdAsync(customerId.Value);
    }

    public async Task<int?> GetCustomerCartIdAsync(ClaimsPrincipal user)
    {
        var customerId = await GetCurrentCustomerIdAsync(user);

        if (customerId is null)
            return null;

        return await unitOfWork.CustomerRepository.GetCustomerCartIdAsync(customerId.Value);
    }
}