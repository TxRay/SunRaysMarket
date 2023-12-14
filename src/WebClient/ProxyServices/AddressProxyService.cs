using System.Net.Http.Json;
using Application.DomainModels;
using Application.DomainModels.Responses;
using Application.Services;

namespace WebClient.ProxyServices;

public class AddressProxyService(HttpClient client) : IAddressService
{
    public async Task<int?> CreateAddressAsync(CreateAddressModel model)
    {
        Console.WriteLine("Creating address...");
        var response = await client
            .PostAsJsonAsync("api/addresses", model)
            .ContinueWith(response =>
            {
                if (!response.Result.IsSuccessStatusCode)
                    throw new HttpRequestException("Failed to create address.");

                return response.Result;
            })
            .ContinueWith(
                resultTask => resultTask.Result.Content.ReadFromJsonAsync<CreateAddressResponse>()
            )
            .Unwrap();

        return response?.AddressId;
    }

    public async Task<AddressModel?> GetAddressAsync(int addressId) =>
        await client.GetFromJsonAsync<AddressModel?>($"api/addresses/{addressId}");
}
