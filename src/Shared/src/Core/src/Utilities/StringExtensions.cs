namespace SunRaysMarket.Shared.Core.Utilities;

public static class StringExtensions
{
    public static string AppendSuffixIfNotNull(
        this string baseString,
        string? suffix,
        string separator = "--"
    )
    {
        return string.IsNullOrEmpty(suffix) ? baseString : $"{baseString}{separator}{suffix}";
    }
}