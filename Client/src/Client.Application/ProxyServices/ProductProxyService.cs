using System.Net.Http.Json;
using SunRaysMarket.Shared.Core.DomainModels;
using SunRaysMarket.Shared.Core.DomainModels.Responses;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Client.Application.ProxyServices;

public class ProductProxyService(HttpClient httpClient) : IProductService
{
    public async Task<ProductDetailsModel?> GetProductDetailsAsync(int productId) =>
        await httpClient
            .GetAsync($"/api/product/{productId}")
            .ContinueWith(messageTask =>
            {
                var message = messageTask.Result;
                message.EnsureSuccessStatusCode();
                return message.Content.ReadFromJsonAsync<ProductDetailsModel>();
            })
            .Unwrap();

    public IAsyncEnumerable<ProductListModel?> GetAllProductsAsync()
     => httpClient
         .GetFromJsonAsAsyncEnumerable<ProductListModel>("/api/products");


    public IAsyncEnumerable<ProductListModel?> GetAllProductsAsync(int departmentId)
        => httpClient
            .GetFromJsonAsAsyncEnumerable<ProductListModel>($"/api/products/{departmentId}");
    public async Task<IEnumerable<ProductListModel>> SearchForProductsAsync(string? queryString)
    {
        var command = new ProductSearchCommand { Query = queryString };
        var productListModel = await httpClient
            .PostAsJsonAsync($"/api/products/search", command)
            .ContinueWith(messageTask =>
            {
                var message = messageTask.Result;
                message.EnsureSuccessStatusCode();
                return message.Content.ReadFromJsonAsync<GetProductListResponse>();
            })
            .Unwrap();

        return productListModel?.Products ?? [];
    }
}
