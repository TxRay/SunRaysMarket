using SunRaysMarket.Server.Application.Services;
using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Server.Application.ServicesImpl.Scoped;

public class CartService(IUnitOfWork unitOfWork, ICookieService cookieService)
    : ICartService
{
    public async Task<IEnumerable<CartItemListModel>> GetCartItemsAsync(int cartId)
        => await unitOfWork.CartRepository.GetCartItemsAsync(cartId);

    public async Task<IEnumerable<CartItemListModel>> GetActiveCartItemsAsync()
        => cookieService.CartId is { } cartId
            ? await GetCartItemsAsync(cartId)
            : [];
    public IAsyncEnumerable<CartItemListModel> GetActiveCartItemsAsyncEnumerable()
        => cookieService.CartId is { } cartId
            ? unitOfWork.CartRepository.GetCartItemsAsyncEnumerable(cartId)
            : AsyncEnumerable.Empty<CartItemListModel>();
}