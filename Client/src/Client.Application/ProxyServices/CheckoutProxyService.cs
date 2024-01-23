using System.Net.Http.Json;
using SunRaysMarket.Shared.Core.DomainModels;
using SunRaysMarket.Shared.Core.DomainModels.Checkout;
using SunRaysMarket.Shared.Core.Enums;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Client.Application.ProxyServices;

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

    public async Task CheckoutAsync(CheckoutSubmitModel model) =>
        await httpClient.PostAsJsonAsync("api/checkout", model);
}
