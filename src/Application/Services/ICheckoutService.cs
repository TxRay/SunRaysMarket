using System.Linq.Expressions;
using System.Security.Claims;
using Application.Builders;
using Application.DomainModels;
using Application.DomainModels.Checkout;
using Application.Enums;
using WebClient.Models;

namespace Application.Services;

public interface ICheckoutService
{
    Task<IEnumerable<TimeSlotListModel>> GetCheckoutTimeSlotsAsync(
        int storeId,
        OrderType orderType
    );

    Task CheckoutAsync(
        int timeSlotId,
        OrderType orderType,
        Action<IAddressBuilder> billingAddressBuilder,
        Action<IPaymentMethodBuilder> paymentMethodBuilder,
        Action<IAddressBuilder>? deliveryAddressBuilder
    );

    Task CheckoutAsync(ClaimsPrincipal user, CheckoutSubmitModel checkoutSubmitModel);
}
