using Microsoft.AspNetCore.Components;

namespace WebClient.Components.Modals;

public abstract class ModalContentBase<TState> : ComponentBase, IDisposable where TState : class, new()
{
    [Inject] private IServiceProvider ServiceProvider { get; set; } = default!;

    private IModalController? _modalController;
    private ModalContext<TState>? _modalContext;

    protected bool IsDisposed { get; private set; }

    IModalController ModalController
    {
        get
        {
            if (ServiceProvider is null)
                throw new InvalidOperationException("ServiceProvider is null");

            ObjectDisposedException.ThrowIf(IsDisposed, this);

            _modalController ??= ServiceProvider.GetRequiredService<IModalController>()
                                 ?? throw new InvalidOperationException(
                                     "No implementation of IModalController was found");
            return _modalController;
        }
    }

    protected ModalContext<TState> ModalContext
    {
        get
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);
            _modalContext ??= ModalController.GetActiveContext<TState>();
            return _modalContext;
        }
    }

    protected override void OnInitialized()
    {
        ModalContext.EventHandlers.OnUnloaded += OnUnloaded;
        ModalContext.EventHandlers.OnUnloadedAsync += OnUnloadedAsync;
    }

    protected virtual void OnUnloaded()
    {
    }


    protected virtual Task OnUnloadedAsync() => Task.CompletedTask;

    protected Task ChangeModalContent<TComponent, TNewState>(ModalOptions? options = default, 
        TNewState? initialState = default, Dictionary<string, object?>? tempData = default)
        where TComponent : IComponent, IDisposable where TNewState : class, new() =>
        ModalController.DispatchAsync<TComponent, TNewState>(options, initialState, tempData);

    protected Task CloseModal()
        => ModalController.Close();


    void IDisposable.Dispose()
    {
        ModalContext.EventHandlers.OnUnloaded -= OnUnloaded;
        ModalContext.EventHandlers.OnUnloadedAsync -= OnUnloadedAsync;
        Dispose(true);
        IsDisposed = true;
    }

    protected virtual void Dispose(bool disposing)
    {
    }
}