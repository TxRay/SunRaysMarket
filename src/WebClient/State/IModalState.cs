namespace WebClient.State;

public interface IModalState
{
    bool ShowModal { get; }
    event Action? OnChange;
}

