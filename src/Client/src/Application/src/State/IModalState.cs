namespace SunRaysMarket.Client.Application.State;

public interface IModalState
{
    bool ShowModal { get; }
    event Action? OnChange;
}