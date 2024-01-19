using System.Net.Http.Json;
using Application.DomainModels;
using Application.EndpointViewModels;
using Application.Services;

namespace WebClient.ProxyServices;

public class CustomerAddressProxyService(HttpClient client) : ICustomerAddressService
{
    public async Task<IEnumerable<AddressModel>> GetAddressesAsync() =>
        (
            await client.GetFromJsonAsync<GetAddressesResponse>("api/addresses/customer")
            ?? new GetAddressesResponse()
        ).Addresses;

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
