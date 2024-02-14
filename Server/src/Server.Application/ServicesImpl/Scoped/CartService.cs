using SunRaysMarket.Server.Application.Services;
using SunRaysMarket.Server.Application.UnitOfWork;

namespace SunRaysMarket.Server.Application.ServicesImpl.Scoped;

public class CartService(IUnitOfWork unitOfWork, ICustomerService customerService, ICookieService cookieService)
    : ICartService
{
    public Task<IEnumerable<CartItemListModel>> GetCartItemsAsync(int cartId)
        => unitOfWork.CartRepository.GetCartItemsAsync(cartId);

    public async Task<IEnumerable<CartItemListModel>> GetActiveCartItemsAsync()
    {
        var cartId = cookieService.CartId;
        return cartId is not null ? await GetCartItemsAsync(cartId.Value) : [];
    }
}