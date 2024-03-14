using System.Net.Http.Json;
using SunRaysMarket.Shared.Core.DomainModels;
using SunRaysMarket.Shared.Core.DomainModels.Responses;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Client.Application.ProxyServicesImpl.Scoped;

public class ProductProxyService(HttpClient httpClient) : IProductService
{
    public async Task<ProductDetailsModel?> GetProductDetailsAsync(int productId)
    {
        return await httpClient
            .GetAsync($"/api/product/{productId}")
            .ContinueWith(messageTask =>
            {
                var message = messageTask.Result;
                message.EnsureSuccessStatusCode();
                return message.Content.ReadFromJsonAsync<ProductDetailsModel>();
            })
            .Unwrap();
    }

    public IAsyncEnumerable<ProductListModel?> GetAllProductsAsync()
    {
        return httpClient.GetFromJsonAsAsyncEnumerable<ProductListModel>("/api/products");
    }

    public IAsyncEnumerable<ProductListModel?> GetAllProductsAsync(int departmentId)
    {
        return httpClient.GetFromJsonAsAsyncEnumerable<ProductListModel>(
            $"/api/products/{departmentId}"
        );
    }
    
}
