using System.Net.Http.Json;
using Application.Builders;
using Application.DomainModels;
using Application.EndpointViewModels;
using Application.Services;

namespace WebClient.ProxyServices;

internal class CartControlsProxyService(HttpClient client) : ICartControlsService
{
    public Task<CartItemControlModel> GetCartItemInfoAsync(int cartItemId)
    {
        throw new NotImplementedException();
    }

    public async Task<AddItemToCartResponse> AddItemToCartAsync(Action<IAddItemToCartOptionsBuilder> buildOptions)
    {
        var optionsBuilder = new AddItemToCarCommandOptionsBuilder();
        buildOptions.Invoke(optionsBuilder);
        optionsBuilder.Build();
        
        var command = optionsBuilder.Options.Command!;
        
        return await client.PostAsJsonAsync("api/cart/add-item", command)
            .ContinueWith(
                messageTask =>
                {
                    var message = messageTask.Result;
                    message.EnsureSuccessStatusCode();
                    return message.Content.ReadFromJsonAsync<AddItemToCartResponse>();
                }
                    
                ).Unwrap()
            ?? throw new InvalidOperationException("AddItemToCartResponse was null");
    }

    public async Task<UpdateCartItemQuantityResponse> UpdateQuantityAsync(UpdateCartItemQuantityCommand command)
    => await client.PostAsJsonAsync("api/cart/update-item-quantity", command)
        .ContinueWith(
            messageTask =>
            {
                var message = messageTask.Result;
                message.EnsureSuccessStatusCode();
                return message.Content.ReadFromJsonAsync<UpdateCartItemQuantityResponse>();
            }
        ).Unwrap()
        ?? throw new InvalidOperationException("UpdateCartItemQuantityResponse was null");

    public Task RemoveItemAsync(RemoveCartItemCommand command)
    {
        Console.WriteLine("RemoveItemAsync");
        
       return client.PostAsJsonAsync("api/cart/remove-item", command)
            .ContinueWith(
                task =>
                {
                    task.Result.EnsureSuccessStatusCode();
                    return Task.CompletedTask;
                })
            .Unwrap();
    }
}