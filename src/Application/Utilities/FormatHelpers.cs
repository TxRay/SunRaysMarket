namespace Application.Utilities;

public static class FormatHelpers
{
    public static string ToCurrencyString(float dollarsDecimal, double exchangeRate = 1.0) =>
        (dollarsDecimal * exchangeRate).ToString("C");

    public static string ToUnitPriceString(
        float dollarsDecimal,
        string unitOfMeasure,
        double exchangeRate = 1.0
    ) => $"{ToCurrencyString(dollarsDecimal, exchangeRate)} / {unitOfMeasure}";

    public static string ToMeasureString(this float measure, string unitOfMeasure) =>
        $"{measure} {unitOfMeasure}";
}
