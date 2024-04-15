using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Net.Http.Headers;
using SunRaysMarket.Client.Web;
using SunRaysMarket.Server.Application.Extensions;
using SunRaysMarket.Server.Application.State;
using SunRaysMarket.Server.Components.Extensions;
using SunRaysMarket.Server.Infrastructure.Extensions;
using SunRaysMarket.Server.Web.Components;
using SunRaysMarket.Server.Web.Endpoints;
using SunRaysMarket.Server.Web.Middleware;
using SunRaysMarket.Server.Web.RenderMethods;
using SunRaysMarket.Server.Web.State;
using SunRaysMarket.Shared.Extensions.RenderMethods;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

var builder = WebApplication.CreateBuilder(args);

// Add configurations
builder.Configuration.AddEnvironmentVariables("SUNRAYSMARKET_");

// Add services to the container.
builder
    .Services
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
        options.Providers.Add<BrotliCompressionProvider>();
        options.Providers.Add<GzipCompressionProvider>();
    }
);

builder.Services.AddResponseCaching();

builder
    .Services
    .AddAntiforgery(options =>
    {
        options.FormFieldName = "RequestVerificationTokenField";
        options.HeaderName = "X-CSRF-TOKEN";
        options.Cookie.Name = "SunRaysMarket.X-CSRF-TOKEN";
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.HttpOnly = true;
    });

builder
    .Services
    .AddSession(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.Name = "SunRaysMarket.Session";
        options.Cookie.IsEssential = true;
        options.IdleTimeout = TimeSpan.FromMinutes(30);
    });

// Add required services from local project assemblies
builder.Services.AddServerInfrastructureAssembly(builder.Configuration);
builder.Services.AddServerApplicationAssembly();
builder.Services.AddServerComponentsAssembly();

builder.Services.AddCustomMiddleware();
builder.Services.AddSingleton<IRenderContext, ServerRenderContext>();
builder.Services.AddScoped<ISessionStateProvider, SessionStateProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
    app.SeedDatabase();
}
else
{
    app.UseExceptionHandler("/error/500/internal-server-error", true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles(
    new StaticFileOptions
    {
        HttpsCompression = HttpsCompressionMode.Compress,
        OnPrepareResponse = (context) =>
        {
            var headers = context.Context.Response.GetTypedHeaders();
            headers.CacheControl = new CacheControlHeaderValue
            {
                Public = true,
                MaxAge = TimeSpan.FromDays(20)
            };
        }
    }
);

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.UseSession();

/*** Custom middleware ***/
app.UseTrackedCookies();
app.UseCustomerPreferences();
app.UseShoppingCart();
app.UseSessionState();
app.UseStatusCodeRedirect();
/************************/

app.UseResponseCompression();

if (!app.Environment.IsDevelopment())
{
    app.UseResponseCaching();
}

app.MapApiEndpoints();
app.MapRazorComponents<App>()
    .WithGroupName("BlazorPageEndpoints")
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(IClientIdentifier).Assembly);

app.Run();