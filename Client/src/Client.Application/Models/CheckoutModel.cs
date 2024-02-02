using SunRaysMarket.Shared.Core.DomainModels.Checkout;

namespace SunRaysMarket.Client.Application.Models;

public class CheckoutModel
{
    public FulfillmentModel FulfillmentInfo { get; set; } = new FulfillmentModel.EmptyModel();
    public string? PaymentMethodId { get; set; }

    public CheckoutSubmitModel ToSubmitModel() =>
        FulfillmentInfo switch
        {
            null => throw new ArgumentNullException(nameof(FulfillmentInfo)),
            FulfillmentModel.EmptyModel => throw new InvalidOperationException(
                "The fulfillment model should not be empty."),
            FulfillmentModel.DeliveryModel deliveryModel =>
                new CheckoutSubmitModel
                {
                    TimeSlotId =
                        deliveryModel.TimeSlotId,
                    OrderType =
                        deliveryModel.OrderType,
                    PaymentMethodId =
                        PaymentMethodId ?? throw new InvalidOperationException("No payment method was set."),
                    DeliveryAddressId = deliveryModel.DeliveryAddressId
                },
            FulfillmentModel.PickupModel pickupModel =>
                new CheckoutSubmitModel
                {
                    TimeSlotId =
                        pickupModel.TimeSlotId,
                    OrderType = pickupModel.OrderType,
                    PaymentMethodId = PaymentMethodId
                                      ?? throw new InvalidOperationException("No payment method was set."),
                },
            _ => throw new ArgumentOutOfRangeException(nameof(FulfillmentInfo))
        };
}