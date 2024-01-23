using SunRaysMarket.Shared.Core.Enums;

namespace SunRaysMarket.Shared.Core.DomainModels.Checkout;

public class CheckoutSubmitModel
{
    public int TimeSlotId { get; set; }
    public OrderType OrderType { get; set; }
    public int BillingAddressId { get; set; }
    public string PaymentMethodId { get; set; } = null!;
    public int? DeliveryAddressId { get; set; }
}
