using System.Net.Http.Json;
using Application.DomainModels;
using Application.EndpointViewModels;
using Application.Services;

namespace WebClient.ProxyServices;

public class ProductProxyService(HttpClient httpClient) : IProductService
{
    public async Task<ProductDetailsModel?> GetProductDetailsAsync(int productId)
        => await httpClient.GetAsync($"/api/product/{productId}")
            .ContinueWith(
                messageTask =>
                {
                    var message = messageTask.Result;
                    message.EnsureSuccessStatusCode();
                    return message.Content.ReadFromJsonAsync<ProductDetailsModel>();
                }
            )
            .Unwrap();

    public async Task<IEnumerable<ProductListModel>> GetAllProductsAsync()
    {
        
        var productListModel = await httpClient.GetAsync($"/api/products")
            .ContinueWith(
                messageTask =>
                {
                    var message = messageTask.Result;
                    message.EnsureSuccessStatusCode();
                    return message.Content.ReadFromJsonAsync<GetProductListResponse>();
                })
            .Unwrap();

        return productListModel?.Products ?? [];
    }

    public async Task<IEnumerable<ProductListModel>> GetAllProductsAsync(int departmentId)
    {
        var productListModel = await httpClient.GetAsync($"/api/products/{departmentId}")
            .ContinueWith(
                messageTask =>
                {
                    var message = messageTask.Result;
                    message.EnsureSuccessStatusCode();
                    return message.Content.ReadFromJsonAsync<GetProductListResponse>();
                })
            .Unwrap();

        return productListModel?.Products ?? [];
    }
}