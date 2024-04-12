namespace SunRaysMarket.Shared.Components.Enums.Mappings;

internal static class LayoutTypeMapping
{
    public static string ToStyleString(this LayoutType layoutType)
    {
        return layoutType switch
        {
            LayoutType.Admin => "layout layout--admin",
            LayoutType.Application => "layout layout--application",
            LayoutType.Page => "layout layout--page",
            _ => throw new ArgumentOutOfRangeException(nameof(layoutType), layoutType, null)
        };
    }
}