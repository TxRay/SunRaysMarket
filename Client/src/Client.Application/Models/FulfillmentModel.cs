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

    public abstract record NonEmptyModel(int TimeSlotId, OrderType OrderType, int? StoreId) : FulfillmentModel
    {
        public abstract bool IsValid { get; }
    }

    public record DeliveryModel(int TimeSlotId, int? StoreId = null, int? DeliveryAddressId = null)
        : NonEmptyModel(TimeSlotId, OrderType.Delivery, StoreId)
    {
        [JsonConstructor]
        public DeliveryModel(int timeSlotId, int? StoreId, int? deliveryAddressId, OrderType orderType)
            : this(timeSlotId, StoreId, deliveryAddressId)
        {
            if (orderType == OrderType.Pickup)
                throw new SerializationException(
                    $"The order type for a delivery order cannot be '{orderType}'."
                    );
        }

        [JsonIgnore]
        public override bool IsValid =>
            TimeSlotId > 0 && DeliveryAddressId is not null && OrderType is OrderType.Delivery;
    }

    public record PickupModel(int TimeSlotId, int? StoreId = null)
        : NonEmptyModel(TimeSlotId, OrderType.Pickup, StoreId)
    {
        [JsonConstructor]
        public PickupModel(int timeSlotId, int? storeId, OrderType orderType)
            : this(timeSlotId, storeId)
        {
            if (orderType == OrderType.Delivery)
                throw new SerializationException(
                    $"The order type for a pickup order cannot be '{orderType}'."
                );
        }

        [JsonIgnore]
        public override bool IsValid => TimeSlotId > 0 && StoreId is not null && OrderType is OrderType.Pickup;
    }

    public static bool IsNullOrEmpty(FulfillmentModel? model) => model is null or EmptyModel;
}
