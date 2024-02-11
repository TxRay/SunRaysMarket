using System.Net.Http.Json;
using SunRaysMarket.Shared.Core.DomainModels;
using SunRaysMarket.Shared.Core.DomainModels.Responses;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Client.Application.ProxyServices;

public class StoreLocationProxyService(HttpClient client) : IStoreLocationService
{
    public async Task<IEnumerable<StoreListModel>> GetStoreLocationsAsync()
        => (await client.GetFromJsonAsync<StoreLocationsResponse>("/api/store-locations"))?
            .StoreLocations ?? [];

    public Task SetPreferredStoreAsync(int storeId)
        => client.PostAsJsonAsync<SetCustomerPreferredStoreCommand>("/api/customer/preferences/store",
            new SetCustomerPreferredStoreCommand
            {
                PreferredStoreId = storeId
            }
        );

    public async Task<int?> GetPreferredStoreAsync()
        => (await client.GetFromJsonAsync<CustomerStorePreferenceResponse>("/api/customer/preferences/store"))
            ?.PreferredStoreId;
}