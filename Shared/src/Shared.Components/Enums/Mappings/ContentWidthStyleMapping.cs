namespace SunRaysMarket.Shared.Components.Enums.Mappings;

internal static class ContentWidthStyleMapping
{
    public static string ToStyleString(this ContentWidth contentWidth) =>
        contentWidth switch
        {
            ContentWidth.PageContent => "layout__content",
            ContentWidth.PageFull => "layout__full",
            _ => throw new ArgumentOutOfRangeException(nameof(contentWidth), contentWidth, null)
        };
}
