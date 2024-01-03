using System.Net.Http.Json;
using Application.Builders;
using Application.DomainModels;
using Application.EndpointViewModels;
using Application.Services;

namespace WebClient.ProxyServices;

internal class CartProxyService(HttpClient client) : ICartService
{
    public async Task<CreateCartResponse> CreateCartAsync() =>
        await client
            .PostAsync("api/cart/create", null)
            .ContinueWith(messageTask =>
            {
                var message = messageTask.Result;
                message.EnsureSuccessStatusCode();
                return message.Content.ReadFromJsonAsync<CreateCartResponse>();
            })
            .Unwrap() ?? throw new InvalidOperationException("CreateCartResponse was null");

    public async Task<CartItemControlModel?> GetCartItemInfoAsync(int cartItemId)
        => await client.GetAsync($"api/cart/item-info/{cartItemId}")
            .ContinueWith(messageTask =>
                {
                    var message = messageTask.Result;
                    message.EnsureSuccessStatusCode();
                    return message.Content.ReadFromJsonAsync<CartItemControlModel>();
                }
            )
            .Unwrap();

    public async Task<IEnumerable<CartItemControlModel>> GetAllCartItemInfoAsync()
    {
        var cartItemInfoListModel =  await client.GetAsync("/api/cart/cart-info")
            .ContinueWith(messageTask =>
                {
                    var message = messageTask.Result;
                    message.EnsureSuccessStatusCode();
                    return message.Content.ReadFromJsonAsync<GetCartItemInfoListResponse>();
                }
                )
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
    ) =>
        await client
            .PostAsJsonAsync("api/cart/update-item-quantity", command)
            .ContinueWith(messageTask =>
            {
                var message = messageTask.Result;
                message.EnsureSuccessStatusCode();
                return message.Content.ReadFromJsonAsync<UpdateCartItemQuantityResponse>();
            })
            .Unwrap()
        ?? throw new InvalidOperationException("UpdateCartItemQuantityResponse was null");

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
