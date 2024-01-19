using Application.DomainModels.Checkout;

namespace WebClient.Models;

public class CheckoutModel
{
    public FulfillmentModel? FulfillmentInfo { get; set; }
    public string? PaymentMethodId { get; set; }

    public CheckoutSubmitModel ToSubmitModel() =>
        new CheckoutSubmitModel
        {
            TimeSlotId =
                FulfillmentInfo?.TimeSlotId
                ?? throw new InvalidOperationException("No time slot was set."),
            OrderType =
                FulfillmentInfo?.OrderType
                ?? throw new InvalidOperationException("No order type was set."),
            PaymentMethodId =
                PaymentMethodId
                ?? throw new InvalidOperationException("No payment method was set."),
            DeliveryAddressId = FulfillmentInfo?.DeliveryAddressId
        };
}
