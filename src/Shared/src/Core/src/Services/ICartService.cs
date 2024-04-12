using SunRaysMarket.Shared.Core.DomainModels;

namespace SunRaysMarket.Shared.Core.Services;

public interface ICartService
{
    Task<IEnumerable<CartItemListModel>> GetCartItemsAsync(int cartId);
    Task<IEnumerable<CartItemListModel>> GetActiveCartItemsAsync();
    IAsyncEnumerable<CartItemListModel> GetActiveCartItemsAsyncEnumerable();
}