namespace Application.Utilities;

public static class FloatExtensions
{
    public static string ToCurrencyString(this float dollarValue, double exchangeRate = 1.0) =>
        (dollarValue * exchangeRate).ToString("C");

    public static string ToUnitPriceString(
        this float dollarValue,
        string unitOfMeasure,
        double exchangeRate = 1.0
    ) => $"{dollarValue.ToCurrencyString(exchangeRate)} / {unitOfMeasure}";

    public static string ToMeasureString(this float measure, string unitOfMeasure) =>
        $"{measure} {unitOfMeasure}";
}
