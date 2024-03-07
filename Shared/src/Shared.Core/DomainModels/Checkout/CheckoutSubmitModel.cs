using SunRaysMarket.Shared.Core.Enums;

namespace SunRaysMarket.Shared.Core.DomainModels.Checkout;

public abstract record CheckoutSubmitModel
{
    public record InvalidModel(string? ValidationErrorMessage = default) : CheckoutSubmitModel;

    public abstract record ValidModel(int TimeSlotId, OrderType OrderType, string PaymentMethodId, string ContactNumber)
        : CheckoutSubmitModel;

    public record DeliveryModel(
        int TimeSlotId,
        string PaymentMethodId,
        string ContactNumber,
        int DeliveryAddressId,
        string? DeliveryInstructions) : ValidModel(TimeSlotId, OrderType.Delivery, PaymentMethodId, ContactNumber);

    public record PickupModel(
        int TimeSlotId,
        string PaymentMethodId,
        string ContactNumber) : ValidModel(TimeSlotId, OrderType.Pickup, PaymentMethodId, ContactNumber);
}