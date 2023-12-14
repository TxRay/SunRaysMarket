using Application.Builders;
using Application.DomainModels;
using Application.EndpointViewModels;
using Application.Options;

namespace Application.Services;

public interface ICartService
{
    Task <CreateCartResponse>CreateCartAsync();
    Task<CartItemControlModel> GetCartItemInfoAsync(int cartItemId);
    Task<AddItemToCartResponse> AddItemToCartAsync(Action<IAddItemToCartOptionsBuilder> buildOptions);
    Task<UpdateCartItemQuantityResponse> UpdateQuantityAsync(UpdateCartItemQuantityCommand command);
    Task RemoveItemAsync(RemoveCartItemCommand command);
}