namespace Application.Utilities.OrderCalculations;

public static class OrderAmountCalculations
{
    public static void CalculateAmounts(
        this IOrderPriceSummary order,
        IEnumerable<IOrderItemAmounts> orderItemAmounts,
        float taxRate = 0.0f
    )
    {
        var orderItemAmountsEnumerable =
            orderItemAmounts as IOrderItemAmounts[] ?? orderItemAmounts.ToArray();
        order.Subtotal = orderItemAmountsEnumerable.Sum(x => x.Price * x.Quantity);
        order.Discount = orderItemAmountsEnumerable.Sum(x => x.Discount * x.Quantity);
        var totalAfterDiscount = order.Subtotal - order.Discount;
        order.Tax = totalAfterDiscount * taxRate;
        order.Total =  totalAfterDiscount + order.Tax;
    }
}
