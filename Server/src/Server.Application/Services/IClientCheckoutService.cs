using System.Linq.Expressions;
using SunRaysMarket.Shared.Core.DomainModels.Checkout;
using SunRaysMarket.Shared.Services.Builders;

namespace SunRaysMarket.Server.Application.Services;

public interface IClientCheckoutService
{
    Task CheckoutAsync(
        TimeSlotStepModel timeSlotInfo,
        Expression<Action<IAddressBuilder>> billingAddressBuilder,
        Expression<Action<IPaymentMethodBuilder>> paymentMethodBuilder,
        Expression<Action<IAddressBuilder>>? deliveryAddressBuilder
    );
}
