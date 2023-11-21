using Application.DomainModels;

namespace Application.Repositories;

public interface ICartRepository
{
    Task<CartDetailsModel?> GetCartDetailsAsync(int cartId);
    Task<IEnumerable<CartItemListModel>> GetCartItemsAsync(int cartId);
    Task<bool> CartExistsAsync(int cartId);
    Task<bool> CreateCartAsync(int? customerId);
    Task<bool> DeleteCartAsync(int cartId);
    Task<bool> AddProductToCartAsync(int cartId, int productId, int quantity);
    Task<bool> RemoveProductFromCartAsync(int cartId, int productId);
    Task<bool> UpdateProductQuantityAsync(int cartId, int productId, int quantity);
}
