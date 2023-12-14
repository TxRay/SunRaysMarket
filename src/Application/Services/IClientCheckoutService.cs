using System.Linq.Expressions;
using Application.Builders;
using WebClient.Models;

namespace Application.Services;

public interface IClientCheckoutService
{
    Task CheckoutAsync(
        TimeSlotStepModel timeSlotInfo,
        Expression<Action<IAddressBuilder>> billingAddressBuilder,
        Expression<Action<IPaymentMethodBuilder>> paymentMethodBuilder,
        Expression<Action<IAddressBuilder>>? deliveryAddressBuilder
    );
}
