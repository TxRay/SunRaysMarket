using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace SunRaysMarket.Client.Components;

public class InputCheckoutOption<TValue> : ComponentBase
{
    [Parameter, EditorRequired]
    public string IdBase { get; set; } = default!;

    [Parameter]
    public TValue? Value { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected string HtmlId => $"{IdBase}--{Value}";

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(1, "div");
        builder.AddAttribute(2, "class", "timeslot");
        builder.OpenComponent<InputRadio<TValue>>(3);
        builder.AddAttribute(4, "id", HtmlId);
        builder.AddAttribute(5, "class", "d-none timeslot__radio");
        builder.AddComponentParameter(6, "Value", Value);
        builder.CloseComponent();
        builder.OpenElement(7, "label");
        builder.AddAttribute(8, "class", "timeslot__label");
        builder.AddAttribute(9, "for", HtmlId);
        builder.AddContent(10, ChildContent);
        builder.CloseElement();
        builder.CloseElement();
    }
}
