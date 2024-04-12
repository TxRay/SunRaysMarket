using System.Text.Json.Serialization;
using SunRaysMarket.Shared.Core.Enums;

namespace SunRaysMarket.Shared.Core.DomainModels.Checkout;

[JsonDerivedType(typeof(InvalidModel), "invalidModel")]
[JsonDerivedType(typeof(DeliveryModel), "deliveryModel")]
[JsonDerivedType(typeof(PickupModel), "pickupModel")]
public abstract record CheckoutSubmitModel
{
    public record InvalidModel(string? ValidationErrorMessage = default) : CheckoutSubmitModel;

    public abstract record ValidModel(
        int TimeSlotId,
        OrderType OrderType,
        int StoreId,
        string PaymentMethodId,
        string ContactNumber
    ) : CheckoutSubmitModel;

    public record DeliveryModel(
        int TimeSlotId,
        int StoreId,
        string PaymentMethodId,
        string ContactNumber,
        int DeliveryAddressId,
        string? DeliveryInstructions
    ) : ValidModel(TimeSlotId, OrderType.Delivery, StoreId, PaymentMethodId, ContactNumber);

    public record PickupModel(
        int TimeSlotId,
        int StoreId,
        string PaymentMethodId,
        string ContactNumber
    ) : ValidModel(TimeSlotId, OrderType.Pickup, StoreId, PaymentMethodId, ContactNumber);
}