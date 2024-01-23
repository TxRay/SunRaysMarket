using SunRaysMarket.Shared.Core.Enums;

namespace SunRaysMarket.Client.Application.Models;

public record FulfillmentModel
{
    public int? TimeSlotId { get; init; }
    public OrderType? OrderType { get; init; }
    public int? DeliveryAddressId { get; init; }
}
