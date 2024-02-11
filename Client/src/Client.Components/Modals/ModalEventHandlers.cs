namespace SunRaysMarket.Client.Components.Modals;

/// <summary>
///     Class for managing modal event handlers.
/// </summary>
public class ModalEventHandlers
{
    public event Action? OnOpen;
    public event Action? OnClose;
    public event Action? OnLoaded;
    public event Action? OnUnloaded;
    public event Action? OnAfterSwitch;
    public event Action? OnBeforeSwitch;
    public event Func<Task>? OnLoadedAsync;
    public event Func<Task>? OnUnloadedAsync;
    public event Func<Task>? OnOpenAsync;
    public event Func<Task>? OnAfterSwitchAsync;
    public event Func<Task>? OnBeforeSwitchAsync;
    public event Func<Task>? OnCloseAsync;

    private Action? GetHandlerAction(ModalEventType eventType)
    {
        return eventType switch
        {
            ModalEventType.Close => OnClose,
            ModalEventType.Open => OnOpen,
            ModalEventType.Loaded => OnLoaded,
            ModalEventType.Unloaded => OnUnloaded,
            ModalEventType.AfterSwitch => OnAfterSwitch,
            ModalEventType.BeforeSwitch => OnBeforeSwitch,
            _ => throw new ArgumentOutOfRangeException(nameof(eventType), eventType, null)
        };
    }

    private Func<Task>? GetHandlerAsyncFunc(ModalEventType eventType)
    {
        return eventType switch
        {
            ModalEventType.Close => OnCloseAsync,
            ModalEventType.Open => OnOpenAsync,
            ModalEventType.AfterSwitch => OnAfterSwitchAsync,
            ModalEventType.BeforeSwitch => OnBeforeSwitchAsync,
            ModalEventType.Loaded => OnLoadedAsync,
            ModalEventType.Unloaded => OnUnloadedAsync,
            _ => throw new ArgumentOutOfRangeException(nameof(eventType), eventType, null)
        };
    }

    /// <summary>
    ///     Invokes the event handlers for the specified event type.
    /// </summary>
    /// <param name="eventType">
    ///     The type of event to invoke the handlers for.
    /// </param>
    /// <returns></returns>
    public Task InvokeEventHandlers(ModalEventType eventType)
    {
        GetHandlerAction(eventType)?.Invoke();
        var handlerTask = GetHandlerAsyncFunc(eventType)?.Invoke();

        return handlerTask ?? Task.CompletedTask;
    }
}