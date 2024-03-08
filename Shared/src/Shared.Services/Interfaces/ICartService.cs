namespace SunRaysMarket.Shared.Services.Interfaces;

public interface ICartService
{
    Task<IEnumerable<CartItemListModel>> GetCartItemsAsync(int cartId);
    Task<IEnumerable<CartItemListModel>> GetActiveCartItemsAsync();
    IAsyncEnumerable<CartItemListModel> GetActiveCartItemsAsyncEnumerable();
}
