using System.Net.Http.Json;
using SunRaysMarket.Shared.Core.Checkout;
using SunRaysMarket.Shared.Core.DomainModels;
using SunRaysMarket.Shared.Core.DomainModels.Checkout;
using SunRaysMarket.Shared.Core.DomainModels.Responses;
using SunRaysMarket.Shared.Core.Enums;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Client.Application.ProxyServicesImpl.Scoped;

internal class CheckoutProxyService(HttpClient httpClient) : ICheckoutService
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

    public async Task<IEnumerable<StoreListModel>> GetStoreLocationsAsync()
    {
        return (
                await httpClient.GetFromJsonAsync<StoreLocationsResponse>("api/checkout/locations")
            )?.StoreLocations ?? [];
    }

    public Task<TimeSlotModel?> GetCheckoutTimeSlotAsync(int id)
    {
        return httpClient.GetFromJsonAsync<TimeSlotModel>($"api/checkout/timeslot/{id}");
    }

    public async Task<CheckoutResponse> CheckoutAsync(CheckoutSubmitModel model)
    {
        var httpResponseMessage = await httpClient.PostAsJsonAsync("api/checkout", model);

        return await httpResponseMessage.Content.ReadFromJsonAsync<CheckoutResponse>()
            ?? new CheckoutResponse.Warning(
                "Something went wrong. No response was returned by the server."
            );
    }
}
