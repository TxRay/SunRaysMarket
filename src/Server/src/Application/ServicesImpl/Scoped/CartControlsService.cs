using Microsoft.AspNetCore.Http;
using SunRaysMarket.Server.Application.Exceptions;
using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Server.Core.Services;
using SunRaysMarket.Shared.Core.Builders;
using SunRaysMarket.Shared.Core.DomainModels.Responses;
using SunRaysMarket.Shared.Core.Services;

namespace SunRaysMarket.Server.Application.ServicesImpl.Scoped;

internal class CartControlsService(
    IHttpContextAccessor httpContextAccessor,
    ICookieService cookieService,
    ICustomerService customerService,
    IUnitOfWork unitOfWork
) : ICartControlsService
{
    public async Task<CreateCartResponse> CreateCartAsync()
    {
        var httpContext =
            httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("HttpContext is null");
        if (httpContext.User.IsAuthenticated())
        {
            await customerService.CreateCustomerCartAsync(httpContext.User);
        }
        else
        {
            await unitOfWork.CartRepository.CreateCartAsync(null, true);
            await unitOfWork.SaveChangesAsync();
        }

        var cartId = unitOfWork.CartRepository.GetPersistedCartId();
        cookieService.CartId = cartId;

        return new CreateCartResponse { CartId = cartId };
    }

    public async Task<CartItemControlModel?> GetCartItemInfoAsync(int cartItemId)
    {
        return (await GetAllCartItemInfoAsync()).FirstOrDefault(ci => ci.Id == cartItemId);
    }

    public async Task DeleteCartAsync()
    {
        var cartId = cookieService.CartId ?? default;

        if (httpContextAccessor.HttpContext?.User is { } user)
        {
            var customerId = await customerService.GetCurrentCustomerIdAsync(user) ?? default;
            await unitOfWork.CustomerRepository.RemoveCartFromCustomerAsync(customerId);
        }

        await unitOfWork.CartRepository.DeleteCartAsync(cartId);
        await unitOfWork.SaveChangesAsync();
        cookieService.DeleteCookie(cookies => cookies.CartId!);
    }

    public async Task<IEnumerable<CartItemControlModel>> GetAllCartItemInfoAsync()
    {
        var cartId = cookieService.CartId;

        if (cartId is null)
            return [];

        return await unitOfWork.CartRepository.GetAllCartItemInfoAsync(cartId.Value);
    }

    public async Task<AddItemToCartResponse> AddItemToCartAsync(
        Action<IAddItemToCartOptionsBuilder> buildOptions
    )
    {
        var optionsBuilder = new AddItemToCarCommandOptionsBuilder();

        buildOptions.Invoke(optionsBuilder);
        optionsBuilder.Build(true);
        var options = optionsBuilder.Options;

        await unitOfWork
            .CartRepository
            .AddProductToCartAsync(
                options.CartId!.Value,
                options.Command!.ProductId,
                options.Command!.Quantity,
                true
            );

        try
        {
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception exc)
        {
            throw new AddItemFailedException(
                "Multiple items of the same product are not allowed in the same cart.",
                exc
            );
        }

        var cartItemId = unitOfWork.CartRepository.GetPersistedCartItemId();

        return new AddItemToCartResponse { ItemId = cartItemId };
    }

    public async Task<UpdateCartItemQuantityResponse> UpdateQuantityAsync(
        UpdateCartItemQuantityCommand command
    )
    {
        await unitOfWork
            .CartRepository
            .UpdateProductQuantityAsync(command.CartItemId, command.NewQuantity, true);
        await unitOfWork.SaveChangesAsync();

        var persistedQuantity = unitOfWork.CartRepository.GetPersistedCartItemQuantity();

        return new UpdateCartItemQuantityResponse
        {
            UpdatedQuantity =
                persistedQuantity is null || persistedQuantity != command.NewQuantity
                    ? command.OldQuantity
                    : persistedQuantity.Value
        };
    }

    public async Task RemoveItemAsync(RemoveCartItemCommand command)
    {
        await unitOfWork.CartRepository.RemoveItemFromCartAsync(command.ItemId);
        await unitOfWork.SaveChangesAsync();
    }
}
