using SunRaysMarket.Server.Application.Preferences;

namespace SunRaysMarket.Server.Web.Middleware;

internal class CustomerPreferencesMiddleware(
    ICookieService cookieService,
    IServiceProvider serviceProvider
) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using var scope = serviceProvider.CreateScope();
        var customerPreferencesService = scope
            .ServiceProvider
            .GetRequiredService<ICustomerPreferencesService>();
        var currentPreferences = DefaultPreferences.Model;

        if (cookieService.Preferences is null)
            currentPreferences = cookieService.Preferences =
                await customerPreferencesService.GetCustomerPreferencesAsync()
                ?? DefaultPreferences.Model;

        await next.Invoke(context);

        if (cookieService.WasCookieUpdated(cs => cs.Preferences))
            await customerPreferencesService.SetCustomerPreferences(
                new UpdateCustomerPreferencesModel
                {
                    PreferredStoreId =
                        cookieService.Preferences.PreferredStoreId
                        ?? currentPreferences.PreferredStoreId!.Value
                }
            );
    }
}
