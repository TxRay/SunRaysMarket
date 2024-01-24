using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;
using SunRaysMarket.Server.Components.Enums;
using SunRaysMarket.Server.Components.Enums.Mappings;

namespace SunRaysMarket.Server.Components;

public class PageLayout : ComponentBase
{
    [Inject] private ILogger<PageLayout>? Logger { get; set; } 
    [Parameter] public string Element { get; set; } = "main";

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> Attributes { get; set; } = new();

    [Parameter] public string CssClasses { get; set; } = string.Empty;
    [Parameter] public LayoutType? LayoutType { get; set; }
    [Parameter] public ContentWidth? ContentWidth { get; set; }
    [Parameter] public bool FitVerticalContent { get; set; } = true;
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private bool ClassesSet { get; set; }
    private const string BaseClass = "layout";
    
    
    private string RenderBaseCssClasses()
    {
        string[] classList = [
            BaseClass,
            LayoutType?.ToStyleString(),
            ContentWidth?.ToStyleString(),
            FitVerticalContent ? "layout__content-vertical" : string.Empty,
            CssClasses.Trim()
        ];

        return string.Join(" ", classList);
    }
    
    
    protected override void OnParametersSet()
    {
        if (!Attributes.TryAdd("class", RenderBaseCssClasses()) && !ClassesSet)
        {
            throw new InvalidOperationException(
                "The 'class' attribute should not be directly set." +
                "Use the 'CssClasses' parameter to apply additional css classes."
            );
        }

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