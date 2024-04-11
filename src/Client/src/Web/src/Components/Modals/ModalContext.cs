namespace SunRaysMarket.Client.Web.Components.Modals;

public record ModalContext
{
    public Type? ContentComponent { get; init; }
    public ModalOptions Options { get; init; } = new();

    public Dictionary<string, object?> TempData { get; internal init; } = null!;
    public bool IsVisible => ContentComponent is not null;

    internal virtual Task InvokeEventAsync(ModalEventType eventType)
    {
        return Task.CompletedTask;
    }

    public Task RequestClose()
    {
        return CloseRequestHandler?.Invoke() ?? Task.CompletedTask;
    }

    internal event Func<Task>? CloseRequestHandler;
}

public record ModalContext<TState> : ModalContext
    where TState : class, new()
{
    public ModalEventHandlers EventHandlers { get; set; } = new();
    public TState State { get; internal set; } = new();

    public void UpdateState(Func<TState, TState> updater)
    {
        State = updater.Invoke(State);
    }

    internal override Task InvokeEventAsync(ModalEventType eventType)
    {
        return EventHandlers.InvokeEventHandlers(eventType);
    }
}
