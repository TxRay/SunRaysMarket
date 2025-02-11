using System.Net.Http.Json;
using SunRaysMarket.Shared.Core.Builders;
using SunRaysMarket.Shared.Core.DomainModels;
using SunRaysMarket.Shared.Core.DomainModels.Responses;
using SunRaysMarket.Shared.Core.Services;

namespace SunRaysMarket.Client.Application.ProxyServicesImpl.Scoped;

internal class CartControlsProxyService(HttpClient client) : ICartControlsService
{
    public async Task<CreateCartResponse> CreateCartAsync()
    {
        return await client
            .PostAsync("api/cart/create", null)
            .ContinueWith(messageTask =>
            {
                var message = messageTask.Result;
                message.EnsureSuccessStatusCode();
                return message.Content.ReadFromJsonAsync<CreateCartResponse>();
            })
            .Unwrap() ?? throw new InvalidOperationException("CreateCartResponse was null");
    }

    public async Task<CartItemControlModel?> GetCartItemInfoAsync(int cartItemId)
    {
        return await client
            .GetAsync($"api/cart/item-info/{cartItemId}")
            .ContinueWith(messageTask =>
            {
                var message = messageTask.Result;
                message.EnsureSuccessStatusCode();
                return message.Content.ReadFromJsonAsync<CartItemControlModel>();
            })
            .Unwrap();
    }

    public async Task DeleteCartAsync()
    {
        await client.DeleteAsync("/api/cart");
    }

    public async Task<IEnumerable<CartItemControlModel>> GetAllCartItemInfoAsync()
    {
        var cartItemInfoListModel = await client
            .GetAsync("/api/cart/item-info")
            .ContinueWith(messageTask =>
            {
                var message = messageTask.Result;
                message.EnsureSuccessStatusCode();
                return message.Content.ReadFromJsonAsync<GetCartItemInfoListResponse>();
            })
            .Unwrap();

        return cartItemInfoListModel?.CartItemInfoList ?? [];
    }

    public async Task<AddItemToCartResponse> AddItemToCartAsync(
        Action<IAddItemToCartOptionsBuilder> buildOptions
    )
    {
        var optionsBuilder = new AddItemToCarCommandOptionsBuilder();
        buildOptions.Invoke(optionsBuilder);
        optionsBuilder.Build();

        var command = optionsBuilder.Options.Command!;

        return await client
            .PostAsJsonAsync("api/cart/add-item", command)
            .ContinueWith(messageTask =>
            {
                var message = messageTask.Result;
                message.EnsureSuccessStatusCode();
                return message.Content.ReadFromJsonAsync<AddItemToCartResponse>();
            })
            .Unwrap() ?? throw new InvalidOperationException("AddItemToCartResponse was null");
    }

    public async Task<UpdateCartItemQuantityResponse> UpdateQuantityAsync(
        UpdateCartItemQuantityCommand command
    )
    {
        return await client
                   .PostAsJsonAsync("api/cart/update-item-quantity", command)
                   .ContinueWith(messageTask =>
                   {
                       var message = messageTask.Result;
                       message.EnsureSuccessStatusCode();
                       return message.Content.ReadFromJsonAsync<UpdateCartItemQuantityResponse>();
                   })
                   .Unwrap()
               ?? throw new InvalidOperationException("UpdateCartItemQuantityResponse was null");
    }

    public Task RemoveItemAsync(RemoveCartItemCommand command)
    {
        Console.WriteLine("RemoveItemAsync");

        return client
            .PostAsJsonAsync("api/cart/remove-item", command)
            .ContinueWith(task =>
            {
                task.Result.EnsureSuccessStatusCode();
                return Task.CompletedTask;
            })
            .Unwrap();
    }
}