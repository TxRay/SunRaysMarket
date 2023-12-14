using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Security.Claims;
using Application.Builders;
using Application.DomainModels;
using Application.DomainModels.Checkout;
using Application.Enums;
using Application.Services;
using WebClient.Models;

namespace WebClient.ProxyServices;

internal class CheckoutProxyService(
    HttpClient httpClient,
    IAddressService addressService,
    IPaymentService paymentService
) : ICheckoutService
{
    public async Task<IEnumerable<TimeSlotListModel>> GetCheckoutTimeSlotsAsync(
        int storeId,
        OrderType orderType
    )
    {
        var result = await httpClient.GetAsync($"api/checkout/timeslots/{storeId}/{orderType}");

        return await result.Content.ReadFromJsonAsync<IEnumerable<TimeSlotListModel>>()
            ?? new List<TimeSlotListModel>();
    }

    public Task CheckoutAsync(ClaimsPrincipal user, CheckoutSubmitModel checkoutSubmitModel) =>
        Task.CompletedTask;

    public async Task CheckoutAsync(
        int timeSlotId,
        OrderType orderType,
        Action<IAddressBuilder> billingAddressBuilder,
        Action<IPaymentMethodBuilder> paymentMethodBuilder,
        Action<IAddressBuilder>? deliveryAddressBuilder
    )
    {
        var billingAddressBuilderInstance = new AddressBuilder(addressService);
        var paymentMethodBuilderInstance = new PaymentMethodBuilder(paymentService);
        var deliveryAddressBuilderInstance = deliveryAddressBuilder is null
            ? null
            : new AddressBuilder(addressService);

        billingAddressBuilder.Invoke(billingAddressBuilderInstance);
        paymentMethodBuilder.Invoke(paymentMethodBuilderInstance);
        deliveryAddressBuilder?.Invoke(deliveryAddressBuilderInstance!);

        await billingAddressBuilderInstance.BuildAsync();
        paymentMethodBuilderInstance.Build();
        await deliveryAddressBuilderInstance?.BuildAsync();

        var checkoutSubmitModel = new CheckoutSubmitModel
        {
            BillingAddressId =
                billingAddressBuilderInstance.AddressId
                ?? throw new NullReferenceException("The billing address must be set."),
            PaymentMethodId =
                paymentMethodBuilderInstance.PaymentMethodId
                ?? throw new NullReferenceException("Invalid payment method."),
            TimeSlotId = timeSlotId,
            OrderType = orderType,
            DeliveryAddressId = deliveryAddressBuilderInstance?.AddressId
        };

        await httpClient.PostAsJsonAsync("api/checkout", checkoutSubmitModel);
    }
}
