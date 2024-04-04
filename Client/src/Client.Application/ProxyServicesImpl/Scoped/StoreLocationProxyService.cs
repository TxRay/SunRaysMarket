using System.Net.Http.Json;
using SunRaysMarket.Shared.Core.DomainModels;
using SunRaysMarket.Shared.Core.DomainModels.Responses;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Client.Application.ProxyServicesImpl.Scoped;

public class StoreLocationProxyService(HttpClient client) : IStoreLocationService
{
    public async Task<IEnumerable<StoreListModel>> GetStoreLocationsAsync()
    {
        return (
                await client.GetFromJsonAsync<StoreLocationsResponse>("/api/store-locations")
            )?.StoreLocations ?? [];
    }

    public Task SetPreferredStoreAsync(int storeId)
    {
        return client.PostAsJsonAsync(
            "/api/customer/preferences/store",
            new SetCustomerPreferredStoreCommand { PreferredStoreId = storeId }
        );
    }

    public async Task<int?> GetPreferredStoreAsync()
    {
        return (
            await client.GetFromJsonAsync<CustomerStorePreferenceResponse>(
                "/api/store-locations/preferred"
            )
        )?.PreferredStoreId;
    }
}
