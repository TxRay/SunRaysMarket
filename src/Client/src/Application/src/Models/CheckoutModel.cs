using SunRaysMarket.Shared.Core.DomainModels.Checkout;

namespace SunRaysMarket.Client.Application.Models;

public class CheckoutModel
{
    public FulfillmentModel FulfillmentInfo { get; set; } = new FulfillmentModel.EmptyModel();
    public string? PaymentMethodId { get; set; }
    public string ContactNumber { get; set; } = default!;
    public string? DeliveryInstructions { get; set; }

    public CheckoutSubmitModel ToSubmitModel()
    {
        if (
            FulfillmentInfo is not FulfillmentModel.NonEmptyModel { IsValid: true }
            || this is { PaymentMethodId: null }
        )
            return new CheckoutSubmitModel.InvalidModel();

        return FulfillmentInfo switch
        {
            null => throw new ArgumentNullException(nameof(FulfillmentInfo)),
            FulfillmentModel.EmptyModel
                => throw new InvalidOperationException(
                    "The fulfillment model should not be empty."
                ),
            FulfillmentModel.DeliveryModel deliveryModel
                => new CheckoutSubmitModel.DeliveryModel(
                    deliveryModel.TimeSlotId,
                    deliveryModel.StoreId!.Value,
                    PaymentMethodId =
                        PaymentMethodId
                        ?? throw new InvalidOperationException("No payment method was set."),
                    ContactNumber,
                    deliveryModel.DeliveryAddressId!.Value,
                    DeliveryInstructions
                ),
            FulfillmentModel.PickupModel pickupModel
                => new CheckoutSubmitModel.PickupModel(
                    pickupModel.TimeSlotId,
                    pickupModel.StoreId!.Value,
                    ContactNumber: ContactNumber,
                    PaymentMethodId: PaymentMethodId!
                ),
            _ => throw new ArgumentOutOfRangeException(nameof(FulfillmentInfo))
        };
    }
}