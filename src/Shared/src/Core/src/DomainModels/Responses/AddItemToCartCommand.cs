namespace SunRaysMarket.Shared.Core.DomainModels.Responses;

public class AddItemToCartCommand
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
}