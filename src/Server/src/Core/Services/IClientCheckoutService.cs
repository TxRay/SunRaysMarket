using System.Linq.Expressions;
using SunRaysMarket.Shared.Core.Builders;
using SunRaysMarket.Shared.Core.DomainModels.Checkout;

namespace SunRaysMarket.Server.Core.Services;

public interface IClientCheckoutService
{
    Task CheckoutAsync(
        TimeSlotStepModel timeSlotInfo,
        Expression<Action<IAddressBuilder>> billingAddressBuilder,
        Expression<Action<IPaymentMethodBuilder>> paymentMethodBuilder,
        Expression<Action<IAddressBuilder>>? deliveryAddressBuilder
    );
}
