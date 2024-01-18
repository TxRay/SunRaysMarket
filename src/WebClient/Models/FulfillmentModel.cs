using Application.Enums;

namespace WebClient.Models;

public record FulfillmentModel
{
    public int? TimeSlotId { get; init; }
    public OrderType? OrderType { get; init; }
    public int? DeliveryAddressId { get; init; }
}