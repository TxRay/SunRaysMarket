namespace SunRaysMarket.Server.Application.Checkout;

internal static class ReadonlyDictionaryExtensions
{
    public static bool TryGetValue<TValue>(
        this IReadOnlyDictionary<Type, object> checkoutResponses,
        out TValue? value
    )
    {
        if (checkoutResponses.TryGetValue(typeof(TValue), out var v) && v is TValue val)
        {
            value = val;
            return true;
        }

        value = default;
        return false;
    }
}
