using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace SunRaysMarket.Client.Components.Modals;

public abstract class ModalContentBase<TState> : ComponentBase, IDisposable
    where TState : class, new()
{
    private ModalContext<TState>? _modalContext;

    private IModalController? _modalController;

    [Inject]
    private IServiceProvider ServiceProvider { get; set; } = default!;

    protected bool IsDisposed { get; private set; }

    private IModalController ModalController
    {
        get
        {
            if (ServiceProvider is null)
                throw new InvalidOperationException("ServiceProvider is null");

            ObjectDisposedException.ThrowIf(IsDisposed, this);

            _modalController ??=
                ServiceProvider.GetRequiredService<IModalController>()
                ?? throw new InvalidOperationException(
                    "No implementation of IModalController was found"
                );
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

    void IDisposable.Dispose()
    {
        ModalContext.EventHandlers.OnUnloaded -= OnUnloaded;
        ModalContext.EventHandlers.OnUnloadedAsync -= OnUnloadedAsync;
        Dispose(true);
        IsDisposed = true;
    }

    protected override void OnInitialized()
    {
        ModalContext.EventHandlers.OnUnloaded += OnUnloaded;
        ModalContext.EventHandlers.OnUnloadedAsync += OnUnloadedAsync;
    }

    protected virtual void OnUnloaded() { }

    protected virtual Task OnUnloadedAsync()
    {
        return Task.CompletedTask;
    }

    protected Task ChangeModalContent<TComponent, TNewState>(
        ModalOptions? options = default,
        TNewState? initialState = default,
        Dictionary<string, object?>? tempData = default
    )
        where TComponent : IComponent, IDisposable
        where TNewState : class, new()
    {
        return ModalController.DispatchAsync<TComponent, TNewState>(
            options,
            initialState,
            tempData
        );
    }

    protected Task CloseModal()
    {
        return ModalController.Close();
    }

    protected virtual void Dispose(bool disposing) { }
}
