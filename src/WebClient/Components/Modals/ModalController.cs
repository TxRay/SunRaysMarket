using Microsoft.AspNetCore.Components;

namespace WebClient.Components.Modals;

/// <summary>
/// Class used to dispatch modal events and close the active modal.
/// </summary>
public class ModalController(ILogger<ModalController> logger) : IModalController
{
    private ModalContext ActiveContext { get; set; } = new();

    /// <summary>
    /// Dispatches a modal event to the active modal context.
    /// </summary>
    /// <param name="options">
    /// The options to be used for the modal.  If null, a new instance of the options class will be created.
    /// </param>
    /// <param name="initialState">
    /// The initial state to be used for the modal.  If null, a new instance of the state class will be created.
    /// </param>
    /// <param name="tempData"></param>
    /// <typeparam name="TComponent">The type of the component to be used as the modal content. </typeparam>
    /// <typeparam name="TState"> The type of the state class to be used for the modal.</typeparam>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.  The task result is the modal context
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the type parameter <typeparamref name="TComponent"/> is not a Blazor component.
    /// </exception>
    public async Task<ModalContext<TState>> DispatchAsync<TComponent, TState>(
        ModalOptions? options,
        TState? initialState = default,
        Dictionary<string, object?>? tempData = default
    )
        where TComponent : IComponent, IDisposable
        where TState : class, new()
    {
        if (!typeof(IComponent).IsAssignableFrom(typeof(TComponent)))
        {
            throw new ArgumentException(
                $"The type {typeof(TComponent).Name} must be a Blazor Component.",
                nameof(TComponent)
            );
        }

        var oldContext = ActiveContext;
        var modalShouldBeOpened = !oldContext.IsVisible;

        if (!modalShouldBeOpened)
        {
            await oldContext.InvokeEventAsync(ModalEventType.Unloaded);
            await oldContext.InvokeEventAsync(ModalEventType.BeforeSwitch);
        }

        oldContext.CloseRequestHandler -= Close;

        var newContext = new ModalContext<TState>
        {
            ContentComponent = typeof(TComponent),
            Options = options ?? new ModalOptions(),
            State = initialState ?? new TState(),
            TempData = tempData ?? new Dictionary<string, object?>()
        };

        newContext.CloseRequestHandler += Close;
        ActiveContext = newContext;

        if (modalShouldBeOpened)
            await newContext.InvokeEventAsync(ModalEventType.Open);
        else
        {
            await newContext.InvokeEventAsync(ModalEventType.AfterSwitch);
        }

        await newContext.InvokeEventAsync(ModalEventType.Loaded);

        NotifyStateChanged();

        return newContext;
    }

    /// <summary>
    /// Closes the active modal.
    /// </summary>
    public async Task Close()
    {
        await ActiveContext.InvokeEventAsync(ModalEventType.Unloaded);
        await ActiveContext.InvokeEventAsync(ModalEventType.Close);
        ActiveContext = new ModalContext();
        NotifyStateChanged();
    }

    /// <summary>
    /// Gets the active modal context in cases where the types of the options and state objects are unknown.
    /// If no modal is active, an empty modal context is returned.
    /// </summary>
    /// <returns>
    /// A <see cref="ModalContext"/> representing the active modal context.
    /// </returns>
    public ModalContext GetActiveContext() => ActiveContext;

    /// <summary>
    /// /// Gets the active modal context in cases where the types of the options and state objects are known.
    /// </summary>
    /// <typeparam name="TState">
    /// The type of the state class to be used for the modal content.
    /// </typeparam>
    /// <returns>
    /// A <see cref="ModalContext{TState}"/> representing the active modal context.
    /// </returns>
    /// <exception cref="InvalidOperationException"></exception>
    public ModalContext<TState> GetActiveContext<TState>()
        where TState : class, new()
    {
        if (ActiveContext is not ModalContext<TState> context)
        {
            throw new InvalidOperationException(
                $"No active modal context of type {typeof(TState).Name}."
            );
        }

        return context;
    }

    /// <summary>
    ///  Event which signals a change in the state of the modal.  Used to trigger a re-render of the modal.
    /// </summary>
    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}
