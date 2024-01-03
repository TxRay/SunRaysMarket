using Application.Builders;
using Application.Cookies;
using Application.DomainModels;
using Application.EndpointViewModels;
using Application.Exceptions;
using Application.Options;
using Application.UnitOfWork;
using Application.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

internal class CartService(
    IHttpContextAccessor httpContextAccessor,
    ICustomerService customerService,
    IUnitOfWork unitOfWork
) : ICartService
{
    public async Task<CreateCartResponse> CreateCartAsync()
    {
        var httpContext =
            httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("HttpContext is null");
        if (httpContext.User.IsAuthenticated())
            await customerService.CreateCustomerCartAsync(httpContext.User);
        else
        {
            await unitOfWork.CartRepository.CreateCartAsync(null, true);
            await unitOfWork.SaveChangesAsync();
        }

        var cartId = unitOfWork.CartRepository.GetPersistedCartId();
        httpContext.Response.Cookies.SetCartIdCookie(cartId);

        return new CreateCartResponse { CartId = cartId };
    }

    
    public async Task<CartItemControlModel?> GetCartItemInfoAsync(int cartItemId)
    {
        var context = httpContextAccessor.HttpContext;
        var cartId = context?.Request.Cookies.GetCartIdCookie();

        return cartId is null
            ? null
            : (await unitOfWork.CartRepository.GetAllCartItemInfoAsync(cartItemId))
            .FirstOrDefault(ci => ci.Id == cartItemId);
    }

    public async Task<IEnumerable<CartItemControlModel>> GetAllCartItemInfoAsync()
    {
        var context = httpContextAccessor.HttpContext;
        var cartId = context?.Request.Cookies.GetCartIdCookie();

        if (cartId is null) return [];

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
        catch (DbUpdateException exc)
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
