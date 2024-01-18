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

    public async Task CheckoutAsync(CheckoutSubmitModel model)
        => await httpClient.PostAsJsonAsync("api/checkout", model);
}