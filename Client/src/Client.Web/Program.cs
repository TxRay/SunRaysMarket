using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SunRaysMarket.Client.Application.Extensions;
using SunRaysMarket.Client.Components.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddClientOnlyApplicationServices();
builder.Services.AddClientComponentServices();

await builder.Build().RunAsync();