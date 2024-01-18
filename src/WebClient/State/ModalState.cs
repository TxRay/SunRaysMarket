namespace WebClient.State;

public class ModalState<TState> : IModalState
    where TState : new()
{
    public TState Value { get; private set; } = new();

    public bool ShowModal { get; private set; }

    public void UpdateState(Func<TState, TState> set)
    {
        Value = set.Invoke(Value);
        AfterStateUpdated();
        NotifyStateChanged();
    }

    public void Toggle()
    {
        ShowModal = !ShowModal;
        NotifyStateChanged();
    }

    public void ForceOpen()
    {
        ShowModal = true;
        NotifyStateChanged();
    }

    public void ForceClose()
    {
        ShowModal = false;
        NotifyStateChanged();
    }
    
    public event Action? OnChange;
    
    private void NotifyStateChanged() => OnChange?.Invoke();

    protected virtual void AfterStateUpdated() { }
}