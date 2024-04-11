using Microsoft.AspNetCore.Components;
using SunRaysMarket.Shared.Core.DomainModels;
using SunRaysMarket.Shared.Core.DomainModels.Responses;
using SunRaysMarket.Shared.Core.Services;

namespace SunRaysMarket.Client.Web.Components.Cart;

public abstract class CartControlsBase : OwningComponentBase<ICartControlsService>
{
    [CascadingParameter]
    private int? CartId { get; set; }

    [Parameter]
    public CartItemControlModel? CartItemInfo { get; set; }

    [Parameter]
    public string CssClasses { get; set; } = string.Empty;

    [Parameter]
    public int ProductId { get; set; }

    [Parameter]
    public EventCallback<int> OnItemDeleted { get; set; }

    protected static string UniqueIdSuffix => Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    protected bool ShowAddToCartButton => CartItemInfo is null;
    protected bool ShowDecrementQuantityButton => CartItemInfo?.Quantity > 1;

    protected async Task OnAddToCartClick()
    {
        Console.WriteLine("Current cart id: " + CartId);
        //var createCartResponse = await Service.CreateCartAsync();
        //CartId = createCartResponse.CartId;

        var response = await Service.AddItemToCartAsync(builder =>
        {
            builder.WithCommand(new AddItemToCartCommand { ProductId = ProductId, Quantity = 1 });

            /*if (HttpContext is null) return;

            var cartId = HttpContext.Request.Cookies.GetCartIdCookie()
                         ?? throw new Exception("CartId cookie not found");
            builder.WithCartId(cartId);*/
        });

        if (response is not null)
            CartItemInfo = new CartItemControlModel { Id = response.ItemId, Quantity = 1 };
    }

    protected async Task OnRemoveFromCartClick()
    {
        if (CartItemInfo is not { Id: > 0 })
            return;

        await Service.RemoveItemAsync(new RemoveCartItemCommand { ItemId = CartItemInfo.Id });

        await OnItemDeleted.InvokeAsync(CartItemInfo.Id);
        CartItemInfo = null;
    }

    protected async Task OnQuantityChange(ChangeEventArgs eventArgs)
    {
        if (CartItemInfo is null || !int.TryParse(eventArgs.Value?.ToString(), out var quantity))
            return;

        if (quantity < 1)
            return;

        var result = await Service.UpdateQuantityAsync(
            new UpdateCartItemQuantityCommand
            {
                CartItemId = CartItemInfo.Id,
                OldQuantity = CartItemInfo.Quantity,
                NewQuantity = quantity
            }
        );

        CartItemInfo.Quantity = result.UpdatedQuantity;
    }

    protected async Task OnDecrementQuantityClick()
    {
        if (CartItemInfo is null || CartItemInfo.Quantity == 1)
            return;

        var oldQuantity = CartItemInfo.Quantity;
        CartItemInfo.Quantity = CartItemInfo.Quantity > 1 ? CartItemInfo.Quantity - 1 : 1;

        var result = await UpdateQuantityAsync(CartItemInfo.Id, oldQuantity, CartItemInfo.Quantity);
    }

    private async Task<UpdateCartItemQuantityResponse> UpdateQuantityAsync(
        int cartItemId,
        int oldQuantity,
        int newQuantity
    )
    {
        return await Service.UpdateQuantityAsync(
            new UpdateCartItemQuantityCommand
            {
                CartItemId = cartItemId,
                OldQuantity = oldQuantity,
                NewQuantity = newQuantity
            }
        );
    }

    protected async Task OnIncrementQuantityClick()
    {
        if (CartItemInfo is null)
            return;

        var oldQuantity = CartItemInfo.Quantity;
        CartItemInfo.Quantity++;

        var result = await UpdateQuantityAsync(CartItemInfo.Id, oldQuantity, CartItemInfo.Quantity);
    }
}
