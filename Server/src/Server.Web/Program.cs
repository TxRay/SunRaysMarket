using SunRaysMarket.Client.Web;
using SunRaysMarket.Server.Application.Extensions;
using SunRaysMarket.Server.Infrastructure.Extensions;
using SunRaysMarket.Server.Web.Components;
using SunRaysMarket.Server.Web.Endpoints;
using SunRaysMarket.Server.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddServerInfrastructureAssembly(builder.Configuration);
builder.Services.AddServerApplicationAssembly();
builder.Services.AddCustomMiddleware();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.UseShoppingCart();

app.MapApiEndpoints();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(IClientIdentifier).Assembly);

app.Run();