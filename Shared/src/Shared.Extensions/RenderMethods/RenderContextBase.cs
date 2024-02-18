namespace SunRaysMarket.Shared.Extensions.RenderMethods;

public abstract class RenderContextBase : IRenderContext
{
    public abstract RenderMethod Method { get; }
    public bool IsServer => Method is RenderMethod.StaticServer or RenderMethod.InteractiveServer;
    public bool IsClient => Method is RenderMethod.InteractiveWasm;
    public bool IsInteractive => Method is RenderMethod.InteractiveServer or RenderMethod.InteractiveWasm;
}