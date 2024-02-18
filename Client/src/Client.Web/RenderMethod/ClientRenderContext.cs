using SunRaysMarket.Shared.Extensions.RenderMethods;

namespace SunRaysMarket.Client.Web.RenderMethod;

public class ClientRenderContext : RenderContextBase
{
    public override Shared.Extensions.RenderMethods.RenderMethod Method =>
        Shared.Extensions.RenderMethods.RenderMethod.InteractiveWasm;
}