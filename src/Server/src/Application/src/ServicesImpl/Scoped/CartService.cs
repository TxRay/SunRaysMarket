using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Server.Core.Services;
using SunRaysMarket.Shared.Core.Services;

namespace SunRaysMarket.Server.Application.ServicesImpl.Scoped;

public class CartService(IUnitOfWork unitOfWork, ICookieService cookieService) : ICartService
{
    public async Task<IEnumerable<CartItemListModel>> GetCartItemsAsync(int cartId)
    {
        return await unitOfWork.CartRepository.GetCartItemsAsync(cartId);
    }

    public async Task<IEnumerable<CartItemListModel>> GetActiveCartItemsAsync()
    {
        return cookieService.CartId is { } cartId ? await GetCartItemsAsync(cartId) : [];
    }

    public IAsyncEnumerable<CartItemListModel> GetActiveCartItemsAsyncEnumerable()
    {
        return cookieService.CartId is { } cartId
            ? unitOfWork.CartRepository.GetCartItemsAsyncEnumerable(cartId)
            : AsyncEnumerable.Empty<CartItemListModel>();
    }
}