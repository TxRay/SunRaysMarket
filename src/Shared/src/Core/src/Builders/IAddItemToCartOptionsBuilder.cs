using SunRaysMarket.Shared.Core.DomainModels.Responses;

namespace SunRaysMarket.Shared.Core.Builders;

public interface IAddItemToCartOptionsBuilder
{
    void WithCartId(int cartId);
    void WithCommand(AddItemToCartCommand command);
}