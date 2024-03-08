namespace SunRaysMarket.Shared.Extensions.RenderMethods;

public interface IRenderContext
{
    RenderMethod Method { get; }
    bool IsServer { get; }
    bool IsClient { get; }
    bool IsInteractive { get; }
}
