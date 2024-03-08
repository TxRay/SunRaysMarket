using SunRaysMarket.Shared.Core.DomainModels.Responses;
using SunRaysMarket.Shared.Services.Builders;

namespace SunRaysMarket.Shared.Services.Interfaces;

/// <summary>
/// </summary>
public interface ICartControlsService
{
    /// <summary>
    ///     Create a new shopping cart.
    /// </summary>
    /// <returns></returns>
    Task<CreateCartResponse> CreateCartAsync();

    /// <summary>
    ///     Fetches the initial state for the cart controls.
    /// </summary>
    /// <param name="cartItemId"></param>
    /// <returns>
    ///     An object containing the initial state of the cart item controls for a specific product if the item exists;
    ///     otherwise: null.
    /// </returns>
    Task<CartItemControlModel?> GetCartItemInfoAsync(int cartItemId);

    Task DeleteCartAsync();

    /// <summary>
    ///     Fetches a the initial state of all cart controls.
    /// </summary>
    /// <returns>
    ///     A list of object containing the initial state of all cart controls.
    /// </returns>
    Task<IEnumerable<CartItemControlModel>> GetAllCartItemInfoAsync();

    /// <summary>
    ///     Add an item to the current shopping cart.
    /// </summary>
    /// <param name="buildOptions">
    /// </param>
    /// <returns>
    /// </returns>
    Task<AddItemToCartResponse> AddItemToCartAsync(
        Action<IAddItemToCartOptionsBuilder> buildOptions
    );

    /// <summary>
    ///     Update the quantity of for item already contained in the carry.
    /// </summary>
    /// <param name="command">
    ///     An object of type <see cref="UpdateCartItemQuantityCommand" />
    ///     containingthe information required to update the item's quantity.
    /// </param>
    /// <returns>
    ///     An object of type <see cref="UpdateCartItemQuantityResponse" /> containing the response to the
    ///     update the request to update the cart item quantity.
    /// </returns>
    Task<UpdateCartItemQuantityResponse> UpdateQuantityAsync(UpdateCartItemQuantityCommand command);

    /// <summary>
    ///     Remove a cart item from the current cart.
    /// </summary>
    /// <param name="command">
    ///     An object of type <see cref="RemoveCartItemCommand" /> containing the information required to
    ///     delete the command.
    /// </param>
    /// <returns>
    /// </returns>
    Task RemoveItemAsync(RemoveCartItemCommand command);
}
