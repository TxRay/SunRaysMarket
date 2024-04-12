namespace SunRaysMarket.Server.Application.State;

/// <summary>
///     The state of the current session.
/// </summary>
public interface ISessionStateProvider
{
    SessionState? State { get; }
}