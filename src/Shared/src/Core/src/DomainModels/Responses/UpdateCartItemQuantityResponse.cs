namespace SunRaysMarket.Shared.Core.DomainModels.Responses;

/// <summary>
///     Holds the data for an API request to update the cart quantity.
/// </summary>
public class UpdateCartItemQuantityResponse
{
    /// <summary>
    ///     The quantity of the cart item after the update operation is applied. It should match the value of
    ///     <see cref="UpdateCartItemQuantityCommand.NewQuantity" />.
    /// </summary>
    public int UpdatedQuantity { get; init; }
}