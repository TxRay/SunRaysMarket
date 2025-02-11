using System.Net.Http.Json;
using SunRaysMarket.Shared.Core.DomainModels;
using SunRaysMarket.Shared.Core.DomainModels.Responses;
using SunRaysMarket.Shared.Core.Services;

namespace SunRaysMarket.Client.Application.ProxyServicesImpl.Scoped;

public class CustomerAddressProxyService(HttpClient client) : ICustomerAddressService
{
    public async Task<IEnumerable<AddressModel>> GetAddressesAsync()
    {
        return (
            await client.GetFromJsonAsync<GetAddressesResponse>("api/addresses/customer")
            ?? new GetAddressesResponse()
        ).Addresses;
    }

    public async Task<int?> AddAddress(CreateAddressModel model)
    {
        var result = await client.PostAsJsonAsync("api/addresses/customer", model);
        var responseJson = await result.Content.ReadFromJsonAsync<CreateAddressResponse>();

        return responseJson?.AddressId;
    }

    public async Task RemoveAddress(int addressId)
    {
        await client.DeleteAsync($"api/addresses/customer/{addressId}");
    }
}