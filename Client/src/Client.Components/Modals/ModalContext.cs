namespace SunRaysMarket.Client.Components.Modals;

public record ModalContext
{
    public Type? ContentComponent { get; init; }
    public ModalOptions Options { get; init; } = new();

    internal virtual Task InvokeEventAsync(ModalEventType eventType) => Task.CompletedTask;

    public Dictionary<string, object?> TempData { get; internal init; } = null!;

    public Task RequestClose() => CloseRequestHandler?.Invoke() ?? Task.CompletedTask;

    internal event Func<Task>? CloseRequestHandler;
    public bool IsVisible => ContentComponent is not null;
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

    internal override Task InvokeEventAsync(ModalEventType eventType) =>
        EventHandlers.InvokeEventHandlers(eventType);
}
