using Microsoft.AspNetCore.Mvc;
using SunRaysMarket.Shared.Core.DomainModels.Responses;

namespace SunRaysMarket.Server.Web.Endpoints;

public static class CustomerEndpoints
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var customerGroup = endpoints.MapGroup("/customer");
        var customerPreferencesGroup = customerGroup.MapGroup("/preferences");

        customerPreferencesGroup.MapGet("/store", HandleGetCustomerStorePreference);
        customerPreferencesGroup.MapPost("/store", HandleSetCustomerStorePreference);

        return endpoints;
    }

    private static async Task<IResult> HandleGetCustomerStorePreference(
        ICustomerPreferencesService customerPreferencesService)
        => Results.Json(
            new CustomerStorePreferenceResponse
            {
                PreferredStoreId = await customerPreferencesService.GetPreferredStoreAsync()
            }
        );

    private static async Task<IResult> HandleSetCustomerStorePreference([FromBody] SetCustomerPreferredStoreCommand command,
        ICustomerPreferencesService customerPreferencesService)
    {
        await customerPreferencesService.SetPreferredStoreAsync(command.PreferredStoreId);

        return Results.StatusCode(201);
    }
}