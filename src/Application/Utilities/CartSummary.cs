using Application.DomainModels;

namespace Application.Utilities;

public static class CartSummary
{
    public static CartSummaryModel GetCartSummary(this IEnumerable<CartItemListModel> cartItems)
    {
        var cartItemListModels = cartItems as CartItemListModel[] ?? cartItems.ToArray();
        var subtotal = cartItemListModels.Sum(cartItem => cartItem.ProductPrice);
        var discount = cartItemListModels.Sum(cartItem => cartItem.Discount);
        var tax = subtotal * 0.07f;
        var totalPrice = subtotal + tax;

        return new CartSummaryModel
        {
            Subtotal = subtotal.ToCurrencyString(),
            Discount = discount.ToCurrencyString(),
            Tax = tax.ToCurrencyString(),
            TotalPrice = totalPrice.ToCurrencyString()
        };
    }
}
