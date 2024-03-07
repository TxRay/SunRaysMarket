namespace SunRaysMarket.Shared.Core.Checkout;

public abstract record CheckoutResponse
{
    public record Failure(string? FailureMessage = default) : CheckoutResponse;

    public record Success(string OrderNumber, string OrderAmount) : CheckoutResponse;

    public record Warning(string? WarningMessage = default) : CheckoutResponse;
}