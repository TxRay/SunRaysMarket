namespace SunRaysMarket.Server.Components.Base;

public sealed class PageStateProvider
{
    private PageState? _state;

    public PageState State
    {
        get => _state ??= new PageState();
        set => _state = value;
    }

    public bool HasState => _state is not null;
}