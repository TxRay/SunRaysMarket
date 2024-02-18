using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SunRaysMarket.Client.Application.Extensions;
using SunRaysMarket.Client.Components.Extensions;
using SunRaysMarket.Client.Web.RenderMethod;
using SunRaysMarket.Shared.Extensions.RenderMethods;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder
    .Services
    .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddClientOnlyApplicationServices();
builder.Services.AddClientComponentServices();
builder.Services.AddSingleton<IRenderContext, ClientRenderContext>();

await builder.Build().RunAsync();
