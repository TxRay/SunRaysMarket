namespace Application.EndpointViewModels;

public class AddItemToCartCommand
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
}
