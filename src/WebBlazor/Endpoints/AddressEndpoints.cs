using Application.DomainModels;
using Application.DomainModels.Responses;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebBlazor.Endpoints;

public static class AddressEndpoints
{
    public static IEndpointRouteBuilder MapAddressEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var addressGroup = endpoints
            .MapGroup("/addresses")
            .WithGroupName("Address")
            .WithDescription("Endpoints for managing addresses.");

        addressGroup.MapPost(
            "/",
            async ([FromBody] CreateAddressModel addressModel, IAddressService addressService) =>
            {
                var addressId = await addressService.CreateAddressAsync(addressModel);

                return Results.Json(new CreateAddressResponse { AddressId = addressId });
            }
        );

        addressGroup.MapGet(
            "/{addressId:int}",
            async ([FromRoute] int addressId, IAddressService addressService) =>
            {
                var address = await addressService.GetAddressAsync(addressId);
                return address is not null ? Results.Json(address) : Results.NotFound();
            }
        );

        return endpoints;
    }
}
