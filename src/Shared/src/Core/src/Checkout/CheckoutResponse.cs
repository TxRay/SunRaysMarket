using System.Text.Json.Serialization;

namespace SunRaysMarket.Shared.Core.Checkout;

[JsonDerivedType(typeof(Failure), "failureResponse")]
[JsonDerivedType(typeof(Success), "successfulResponse")]
[JsonDerivedType(typeof(Warning), "warningResponse")]
public abstract record CheckoutResponse
{
    public record Failure(string? FailureMessage = default) : CheckoutResponse;

    public record Success(string OrderNumber, string OrderAmount) : CheckoutResponse;

    public record Warning(string? WarningMessage = default) : CheckoutResponse;
}
