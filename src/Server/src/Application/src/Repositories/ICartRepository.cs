using SunRaysMarket.Server.Core.DomainModels;

namespace SunRaysMarket.Server.Application.Repositories;

public interface ICartRepository
{
    Task<CartDetailsModel?> GetCartDetailsAsync(int cartId, bool persist = false);
    Task<IEnumerable<CartItemListModel>> GetCartItemsAsync(int cartId);
    IAsyncEnumerable<CartItemListModel> GetCartItemsAsyncEnumerable(int cartId);
    Task<IEnumerable<CartItemControlModel>> GetAllCartItemInfoAsync(int cartId);
    Task<CartItemControlModel?> GetCartItemControlInfoAsync(int cartId, int productId);
    Task<bool> CartExistsAsync(int cartId);
    Task CreateCartAsync(int? customerId, bool persist = false);
    Task<bool> DeleteCartAsync(int cartId);
    Task AddProductToCartAsync(int cartId, int productId, int quantity, bool persist = false);
    Task RemoveItemFromCartAsync(int itemId);
    Task RemoveItemFromCartAsync(int cartId, int productId);

    Task UpdateProductQuantityAsync(int itemId, int quantity, bool persist = false);
    int GetPersistedCartId();
    CartDetailsModel GetPersistedCartDetails();

    int GetPersistedCartItemId();

    void ClearPersistedCart();

    int? GetPersistedCartItemQuantity();
}