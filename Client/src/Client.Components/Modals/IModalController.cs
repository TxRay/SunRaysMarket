using Microsoft.AspNetCore.Components;

namespace SunRaysMarket.Client.Components.Modals;

public interface IModalController
{
    /// <summary>
    ///     Dispatches a component and displays its contents inside of the global modal component.
    /// </summary>
    /// <param name="options">
    ///     Object of type <see cref="ModalOptions" /> containing options for the modal content component.
    /// </param>
    /// <param name="initialState">
    ///     Object containing the initial state to for the content component.
    /// </param>
    /// <param name="tempData"></param>
    /// <typeparam name="TComponent">The type of the content component.</typeparam>
    /// <typeparam name="TState">The type of the state object.</typeparam>
    /// <returns>
    ///     An object containing the current modal context.
    /// </returns>
    Task<ModalContext<TState>> DispatchAsync<TComponent, TState>(
        ModalOptions? options,
        TState? initialState = default,
        Dictionary<string, object?>? tempData = default
    )
        where TComponent : IComponent, IDisposable
        where TState : class, new();

    /// <summary>
    ///     Closes the modal modal.
    /// </summary>
    /// <returns>
    ///     An empty Task.
    /// </returns>
    Task Close();

    /// <summary>
    ///     Returns the currently active context in cases where the types of the state and options objects
    ///     are not know.
    /// </summary>
    /// <returns> An object with a type which implements ModalContextBase.</returns>
    ModalContext GetActiveContext();

    /// <summary>
    ///     Returns the currently active context object while providing access to the options and state objects.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <returns></returns>
    ModalContext<TState> GetActiveContext<TState>()
        where TState : class, new();

    /// <summary>
    ///     Event which signals a change in the state of the modal.  Used to trigger a re-render of the modal.
    /// </summary>
    internal event Action? OnChange;
}
