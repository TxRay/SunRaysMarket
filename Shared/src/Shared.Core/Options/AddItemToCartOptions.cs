using SunRaysMarket.Shared.Core.DomainModels.Responses;

namespace SunRaysMarket.Shared.Core.Options;

public class AddItemToCartOptions
{
    public int? CartId { get; set; }
    public AddItemToCartCommand? Command { get; set; }
}
