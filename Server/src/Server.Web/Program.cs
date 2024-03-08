using SunRaysMarket.Client.Web;
using SunRaysMarket.Server.Application.Extensions;
using SunRaysMarket.Server.Infrastructure.Extensions;
using SunRaysMarket.Server.Web.Components;
using SunRaysMarket.Server.Web.Endpoints;
using SunRaysMarket.Server.Web.Middleware;
using SunRaysMarket.Server.Web.RenderMethods;
using SunRaysMarket.Shared.Extensions.RenderMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

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

builder.Services.AddServerInfrastructureAssembly(builder.Configuration);
builder.Services.AddServerApplicationAssembly();
builder.Services.AddCustomMiddleware();
builder.Services.AddSingleton<IRenderContext, ServerRenderContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
    app.SeedDatabase();
}
else
{
    app.UseExceptionHandler("/Error", true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.UseSession();

app.UseTrackedCookies();
app.UseCustomerPreferences();
app.UseShoppingCart();

app.MapApiEndpoints();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(IClientIdentifier).Assembly);

/*app.UseStatusCodePages(context =>
{
    var redirectRoute = context.HttpContext.Response.StatusCode switch
    {
        404 => "/error/404/not-found",
        500 => "error/500/internal-server-error",
        _ => $"error/{context.HttpContext.Response.StatusCode}"
    };

    context.HttpContext.Response.Redirect(redirectRoute);

    return Task.CompletedTask;
});*/

app.Run();
