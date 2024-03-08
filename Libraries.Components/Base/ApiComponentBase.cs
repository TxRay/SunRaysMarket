using Microsoft.AspNetCore.Components;

namespace SunRaysMarket.Libraries.Components;

public abstract class ApiComponentBase : ExtendedComponentBase
{
    [Parameter]
    public virtual UiState ComponentUiState { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public RenderFragment? LoadingContent { get; set; }

    [Parameter]
    public RenderFragment? ErrorContent { get; set; }
}
