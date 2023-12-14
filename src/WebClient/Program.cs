using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebClient.Extensions;
using WebClient.Stores;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder
    .Services
    .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddWebClientServices();
builder.Services.AddWebClientOnlyServices();

await builder.Build().RunAsync();
