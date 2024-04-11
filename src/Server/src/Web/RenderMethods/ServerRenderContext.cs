using SunRaysMarket.Shared.Extensions.RenderMethods;

namespace SunRaysMarket.Server.Web.RenderMethods;

public class ServerRenderContext(IHttpContextAccessor accessor) : RenderContextBase
{
    public override RenderMethod Method =>
        accessor.HttpContext?.Response.HasStarted ?? false
            ? RenderMethod.InteractiveServer
            : RenderMethod.StaticServer;
}
