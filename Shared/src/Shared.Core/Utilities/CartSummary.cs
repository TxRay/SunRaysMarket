using SunRaysMarket.Shared.Core.DomainModels;

namespace SunRaysMarket.Shared.Core.Utilities;

public static class CartSummary
{
    public static CartSummaryModel GetCartSummary(this IEnumerable<CartItemListModel> cartItems)
    {
        var cartItemListModels = cartItems as CartItemListModel[] ?? cartItems.ToArray();
        var subtotal = cartItemListModels.Sum(
            cartItem => (cartItem.ProductPrice * cartItem.Quantity)
        );
        var discount = cartItemListModels.Sum(cartItem => (cartItem.Discount * cartItem.Quantity));
        var tax = subtotal * 0.07f;
        var totalPrice = subtotal + tax;

        return new CartSummaryModel
        {
            Subtotal = FormatHelpers.ToCurrencyString(subtotal),
            Discount = FormatHelpers.ToCurrencyString(discount),
            Tax = FormatHelpers.ToCurrencyString(tax),
            TotalPrice = FormatHelpers.ToCurrencyString(totalPrice)
        };
    }
}
