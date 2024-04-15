using System.Text.Json;
using SunRaysMarket.Server.Application.State;

namespace SunRaysMarket.Server.Web.State;

public class SessionStateProvider(IHttpContextAccessor httpContextAccessor) : ISessionStateProvider
{
    private SessionState? _sessionState;

    public SessionState? State
    {
        get
        {
            if (httpContextAccessor.HttpContext is null)
                throw new InvalidOperationException("The HTTP context is not available.");

            if (
                !httpContextAccessor
                    .HttpContext
                    .Session
                    .TryGetValue("SessionState", out var sessionStateByteArray)
            )
                throw new InvalidOperationException("The session state is not available.");

            _sessionState ??= JsonSerializer.Deserialize<SessionState>(sessionStateByteArray);

            return _sessionState;
        }
    }
}