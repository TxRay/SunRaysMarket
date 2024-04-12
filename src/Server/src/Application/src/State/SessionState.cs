namespace SunRaysMarket.Server.Application.State;

/// <summary>
///     The state of the current session.
/// </summary>
public sealed class SessionState
{
    /// <summary>
    ///     The ID of the user that is currently authorized.
    /// </summary>
    public int? AuthorizedUserId { get; init; }

    /// <summary>
    ///     The ID of the customer that is currently authorized.
    /// </summary>
    public int? AuthorizedCustomerId { get; init; }

    /// <summary>
    ///     The ID of the cart that is currently active.
    /// </summary>
    public int? ActiveCartId { get; init; }

    /// <summary>
    ///     The return URL for the previous active page. Authentication pages such as the
    ///     sign up and login pages are not recorded.
    /// </summary>
    public string? PageReturnUrl { get; init; }
}