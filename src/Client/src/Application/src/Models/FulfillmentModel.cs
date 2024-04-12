using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SunRaysMarket.Shared.Core.Enums;

namespace SunRaysMarket.Client.Application.Models;

[JsonDerivedType(typeof(DeliveryModel), "deliveryModel")]
[JsonDerivedType(typeof(PickupModel), "pickupModel")]
public abstract record FulfillmentModel
{
    public static bool IsNullOrEmpty(FulfillmentModel? model)
    {
        return model is null or EmptyModel;
    }

    public record EmptyModel : FulfillmentModel;

    public abstract record NonEmptyModel(int TimeSlotId, OrderType OrderType, int? StoreId)
        : FulfillmentModel
    {
        public abstract bool IsValid { get; }
    }

    public record DeliveryModel(int TimeSlotId, int? StoreId = null, int? DeliveryAddressId = null)
        : NonEmptyModel(TimeSlotId, OrderType.Delivery, StoreId)
    {
        [JsonConstructor]
        public DeliveryModel(
            int timeSlotId,
            int? StoreId,
            int? deliveryAddressId,
            OrderType orderType
        )
            : this(timeSlotId, StoreId, deliveryAddressId)
        {
            if (orderType == OrderType.Pickup)
                throw new SerializationException(
                    $"The order type for a delivery order cannot be '{orderType}'."
                );
        }

        [JsonIgnore]
        public override bool IsValid =>
            this
                is
                {
                    TimeSlotId: > 0,
                    DeliveryAddressId: > 0,
                    OrderType: OrderType.Delivery,
                    StoreId: > 0
                };
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
        public override bool IsValid =>
            this is { TimeSlotId: > 0, OrderType: OrderType.Pickup, StoreId: > 0 };
    }
}