namespace Application.EndpointViewModels;

/// <summary>
/// Carries the information required to update the quantity of an item already contained in a shopping cart.
/// </summary>
public class UpdateCartItemQuantityCommand
{
    /// <summary>
    /// The DB generated id of the cart item to be updated.
    /// </summary>
    public int CartItemId { get; init; }

    /// <summary>
    /// The current quantity of the item before the update operation is performed.
    /// </summary>
    public int OldQuantity { get; init; }

    /// <summary>
    /// The new quantity of the item.
    /// </summary>
    public int NewQuantity { get; init; }
}
