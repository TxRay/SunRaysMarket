using System.Net.Http.Json;
using SunRaysMarket.Shared.Core.DomainModels;
using SunRaysMarket.Shared.Core.Services;

namespace SunRaysMarket.Client.Application.ProxyServicesImpl.Scoped;

public class CartProxyService(HttpClient client) : ICartService
{
    public async Task<IEnumerable<CartItemListModel>> GetCartItemsAsync(int cartId)
    {
        return await client.GetFromJsonAsync<IEnumerable<CartItemListModel>>(
            $"api/cart/items/{cartId}"
        ) ?? [];
    }

    public async Task<IEnumerable<CartItemListModel>> GetActiveCartItemsAsync()
    {
        return await client.GetFromJsonAsync<IEnumerable<CartItemListModel>>("api/cart/items")
               ?? [];
    }

    public async IAsyncEnumerable<CartItemListModel> GetActiveCartItemsAsyncEnumerable()
    {
        await foreach (
            var item in client.GetFromJsonAsAsyncEnumerable<CartItemListModel>(
                "api/cart/items/streamed"
            )
        )
            if (item is not null)
                yield return item;
            else
                yield break;
    }
}