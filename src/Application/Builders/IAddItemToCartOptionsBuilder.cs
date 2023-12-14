using Application.EndpointViewModels;

namespace Application.Builders;

public interface IAddItemToCartOptionsBuilder
{
    void WithCartId(int cartId);
    void WithCommand(AddItemToCartCommand command);
}