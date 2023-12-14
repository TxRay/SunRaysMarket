using Application.Builders;
using Application.DomainModels;
using Application.EndpointViewModels;
using Application.Exceptions;
using Application.Options;
using Application.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

internal class CartControlService(IUnitOfWork unitOfWork) : ICartControlsService
{
    public Task<CartItemControlModel> GetCartItemInfoAsync(int cartItemId)
    {
        throw new NotImplementedException();
    }


    public async Task<AddItemToCartResponse> AddItemToCartAsync(Action<IAddItemToCartOptionsBuilder> buildOptions)
    {
        var optionsBuilder = new AddItemToCarCommandOptionsBuilder();

        buildOptions.Invoke(optionsBuilder);
        optionsBuilder.Build(true);
        var options = optionsBuilder.Options;

        await unitOfWork
            .CartRepository
            .AddProductToCartAsync(options.CartId!.Value, options.Command!.ProductId, options.Command!.Quantity, true);

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

    public async Task<UpdateCartItemQuantityResponse> UpdateQuantityAsync(UpdateCartItemQuantityCommand command)
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