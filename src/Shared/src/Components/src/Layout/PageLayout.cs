using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;
using SunRaysMarket.Shared.Components.Enums;
using SunRaysMarket.Shared.Components.Enums.Mappings;

namespace SunRaysMarket.Shared.Components;

public class PageLayout : ComponentBase
{
    private const string BaseClass = "layout";

    [Inject] private ILogger<PageLayout>? Logger { get; set; }

    [Parameter] public string Element { get; set; } = "div";

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> Attributes { get; set; } = new();

    [Parameter] public bool UseDefaultPadding { get; set; }

    [Parameter] public string CssClasses { get; set; } = string.Empty;

    [Parameter] public LayoutType LayoutType { get; set; } = LayoutType.Page;

    [Parameter] public ContentWidth? ContentWidth { get; set; }

    [Parameter] public bool FitVerticalContent { get; set; } = true;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    private bool ClassesSet { get; set; }

    private string? VerticalLayoutClass
    {
        get
        {
            if (!FitVerticalContent || LayoutType != LayoutType.Page)
                return null;

            return "layout__content-vertical" + (Element == "main" ? "--main" : string.Empty);
        }
    }

    private string? DefaultPaddingClass => !UseDefaultPadding ? null : "layout__content--p-default";

    private string RenderBaseCssClasses()
    {
        string[] classList =
        [
            BaseClass,
            LayoutType.ToStyleString(),
            ContentWidth?.ToStyleString(),
            VerticalLayoutClass,
            DefaultPaddingClass,
            CssClasses.Trim()
        ];

        return string.Join(" ", classList).Trim();
    }

    protected override void OnParametersSet()
    {
        if (!Attributes.TryAdd("class", RenderBaseCssClasses()) && !ClassesSet)
            throw new InvalidOperationException(
                "The 'class' attribute should not be directly set."
                + "Use the 'CssClasses' parameter to apply additional css classes."
            );

        ClassesSet = true;
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, Element);
        builder.AddMultipleAttributes(1, Attributes);
        builder.AddContent(2, ChildContent);
        builder.CloseElement();
    }
}