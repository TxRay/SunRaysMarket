using System.Net.Http.Json;
using SunRaysMarket.Shared.Core.DomainModels.Responses;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Client.Application.ProxyServicesImpl.Scoped;

public class CustomerPreferencesProxyService(HttpClient client) : ICustomerPreferencesService
{
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