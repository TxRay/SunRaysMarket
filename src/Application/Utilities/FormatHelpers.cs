namespace Application.Utilities;

public static class FormatHelpers
{
    public static string ToCurrencyString(float dollarsDecimal, double exchangeRate = 1.0)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(dollarsDecimal, 0.0f);

        var currencyDecimal = dollarsDecimal * exchangeRate;

        return currencyDecimal > 1.0f
            ? currencyDecimal.ToString("C")
            : $"{(currencyDecimal * 100):G2} \u00A2";
    }
    
    public static string ToUnitPriceString(
        float dollarsDecimal,
        string unitOfMeasure,
        double exchangeRate = 1.0
    ) => $"{ToCurrencyString(dollarsDecimal, exchangeRate)} / {unitOfMeasure}";

    public static string ToMeasureString(this float measure, string unitOfMeasure) =>
        $"{measure} {unitOfMeasure}";

    public static string ShortenCardNumber(string cardNumber) => $"*{cardNumber[^4..]}";
}