namespace Application.EndpointViewModels;

public class UpdateCartItemQuantityCommand
{
    public int CartItemId { get; init; }
    public int OldQuantity { get; init; }
    public int NewQuantity { get; init; }
}
