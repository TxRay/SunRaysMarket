using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SunRaysMarket.Shared.Core.Enums;

namespace SunRaysMarket.Client.Application.Models;

[JsonDerivedType(typeof(DeliveryModel), "deliveryModel")]
[JsonDerivedType(typeof(PickupModel), "pickupModel")]
public abstract record FulfillmentModel
{
    public record EmptyModel : FulfillmentModel;

    public abstract record NonEmptyModel(int TimeSlotId, OrderType OrderType) : FulfillmentModel;

    public record DeliveryModel(int TimeSlotId, int? DeliveryAddressId = default)
        : NonEmptyModel(TimeSlotId, OrderType.Delivery)
    {
        [JsonConstructor]
        public DeliveryModel(int timeSlotId, int? deliveryAddressId, OrderType orderType)
            : this(timeSlotId, deliveryAddressId)
        {
            if (orderType == OrderType.Pickup)
                throw new SerializationException();
        }
    }

    public record PickupModel(int TimeSlotId, int? StoreId = default)
        : NonEmptyModel(TimeSlotId, OrderType.Pickup)
    {
        [JsonConstructor]
        public PickupModel(int timeSlotId, int? storeId, OrderType orderType)
            : this(timeSlotId, storeId)
        {
            if (orderType == OrderType.Delivery)
                throw new SerializationException();
        }
    }

    public static bool IsNullOrEmpty(FulfillmentModel? model) => model is null or EmptyModel;
}
