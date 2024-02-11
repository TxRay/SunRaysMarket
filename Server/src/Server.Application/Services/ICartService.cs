namespace SunRaysMarket.Server.Application.Services;

public interface ICartService
{
    Task<IEnumerable<CartItemListModel>> GetCartItemsAsync(int cartId);
    Task<IEnumerable<CartItemListModel>> GetActiveCartItemsAsync();
}