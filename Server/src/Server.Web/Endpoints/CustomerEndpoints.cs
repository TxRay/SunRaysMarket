using Microsoft.AspNetCore.Mvc;
using SunRaysMarket.Server.Application.Preferences;
using SunRaysMarket.Shared.Core.DomainModels.Responses;

namespace SunRaysMarket.Server.Web.Endpoints;

internal static class CustomerEndpoints
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var customerGroup = endpoints.MapGroup("/customer");
        var customerPreferencesGroup = customerGroup.MapGroup("/preferences");

        customerPreferencesGroup.MapGet("/store", HandleGetCustomerStorePreference);
        customerPreferencesGroup.MapPost("/store", HandleSetCustomerStorePreference);

        return endpoints;
    }

    private static IResult HandleGetCustomerStorePreference(
        ICookieService cookieService)
        => Results.Json(
            new CustomerStorePreferenceResponse
            {
                PreferredStoreId = cookieService.Preferences?.PreferredStoreId
            }
        );

    private static IResult HandleSetCustomerStorePreference([FromBody] SetCustomerPreferredStoreCommand command,
        ICookieService cookieService)
    {
        var preferences = cookieService.Preferences ?? DefaultPreferences.Model;
        preferences.PreferredStoreId = command.PreferredStoreId;
        cookieService.Preferences = preferences;
        
        return Results.StatusCode(201);
    }
}