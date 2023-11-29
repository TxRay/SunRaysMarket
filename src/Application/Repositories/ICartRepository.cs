using Application.DomainModels;

namespace Application.Repositories;

public interface ICartRepository
{
    Task<CartDetailsModel?> GetCartDetailsAsync(int cartId, bool persist = false);
    Task<IEnumerable<CartItemListModel>> GetCartItemsAsync(int cartId);
    Task<CartItemControlModel?> GetCartItemControlInfoAsync(int cartId, int productId);
    Task<bool> CartExistsAsync(int cartId);
    Task CreateCartAsync(int? customerId, bool persist = false);
    Task<bool> DeleteCartAsync(int cartId);
    Task AddProductToCartAsync(int cartId, int productId, int quantity, bool persist = false);
    Task RemoveItemFromCartAsync(int itemId);
    Task RemoveItemFromCartAsync(int cartId, int productId);

    Task UpdateProductQuantityAsync(int itemId, int quantity);
    int GetPersistedCartId();
    CartDetailsModel GetPersistedCartDetails();
    
    int GetPersistedCartItemId();
    
    void ClearPersistedCart();
    
    void GetPersistedCartItem();
    
}
