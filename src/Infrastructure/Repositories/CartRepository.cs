using Application.DomainModels;
using Application.Repositories;

namespace Infrastructure.Repositories;

internal class CartRepository : ICartRepository
{
    public Task<CartDetailsModel?> GetCartDetailsAsync(int cartId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CartItemListModel>> GetCartItemsAsync(int cartId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CartExistsAsync(int cartId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateCartAsync(int? customerId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCartAsync(int cartId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddProductToCartAsync(int cartId, int productId, int quantity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveProductFromCartAsync(int cartId, int productId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProductQuantityAsync(int cartId, int productId, int quantity)
    {
        throw new NotImplementedException();
    }
}
