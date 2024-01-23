using SunRaysMarket.Shared.Core.DomainModels.Responses;
using SunRaysMarket.Shared.Core.Options;

namespace SunRaysMarket.Shared.Services.Builders;

public class AddItemToCarCommandOptionsBuilder : IAddItemToCartOptionsBuilder
{
    public AddItemToCartOptions Options { get; } = new();

    public void WithCartId(int cartId)
    {
        Options.CartId = cartId;
    }
    
    public void WithCommand(AddItemToCartCommand command)
    {
        Options.Command = command;
    }
    
    public void Build(bool requireCartId = false)
    {
        if (requireCartId && Options.CartId is null)
            throw new ArgumentNullException(nameof(Options.CartId), "CartId must be set.");

        if (Options.Command is null)
            throw new ArgumentNullException(nameof(Options.Command), "Command must be set.");
    }
}