using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.CompilerServices;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;
using RuntimeHelpers = Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers;

namespace SunRaysMarket.Client.Components;

public class InputCheckoutOptionGroup<TValue> : ComponentBase
{
    [Inject] private ILogger<InputCheckoutOption<TValue>>? Logger { get; set; }
    [Parameter] public TValue? Value { get; set; }
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<InputRadioGroup<TValue>>(1);
        builder.AddAttribute(2, "class", "timeslots-display");
        builder.AddComponentParameter(4, "ValueChanged",
            EventCallback.Factory.Create<TValue>(this, value =>
            {
                Logger?.LogInformation("Running callback.");
                Value = value;
                ValueChanged.InvokeAsync(Value);
                
            }));
        builder.AddComponentParameter(5, "ValueExpression",
            RuntimeHelpers.TypeCheck<Expression<Func<TValue>>>(
                () => Value
            )
        );
        builder.AddComponentParameter(6, "Value", Value);
        builder.AddAttribute(7, "ChildContent",
            (RenderFragment)(radioGroupBuilder =>
            {
                radioGroupBuilder.OpenElement(8, "div");
                radioGroupBuilder.AddAttribute(9, "class", "timeslots-display");
                radioGroupBuilder.AddContent(10, ChildContent);
                radioGroupBuilder.CloseElement();
            })
        );
        builder.CloseComponent();
    }
}

/*
 <InputRadioGroup class="timeslots-display" Value="Value" ValueChanged="ValueChanged">
       <div class="timeslots-display">
           @ChildContent
       </div>
   </InputRadioGroup>

   @code {
       [Parameter] public TValue? Value { get; set; }
       [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
       [Parameter] public RenderFragment? ChildContent { get; set; }
   }
 */