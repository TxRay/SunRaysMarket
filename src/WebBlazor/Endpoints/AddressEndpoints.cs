using Application.DomainModels;
using Application.DomainModels.Responses;
using Application.EndpointViewModels;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using CreateAddressResponse = Application.DomainModels.Responses.CreateAddressResponse;

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

        addressGroup.MapCustomerAddressEndpoints();

        return endpoints;
    }

    private static IEndpointRouteBuilder MapCustomerAddressEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var customerAddressGroup = endpoints.MapGroup("/customer")
            .WithGroupName("CustomerAddress")
            .WithDescription("Handles customer address requests");

        customerAddressGroup.MapGet("/",
            async (ICustomerAddressService customerAddressService) =>
                Results.Json(
                    new GetAddressesResponse
                    {
                        Addresses = await customerAddressService.GetAddressesAsync()
                    }
                )
        );

        customerAddressGroup.MapPost("/",
            async ([FromBody] CreateAddressModel model, ICustomerAddressService customerAddressService) =>
            Results.Json(
                new CreateAddressResponse
                {
                    AddressId = await customerAddressService.AddAddress(model)
                })
        );

        customerAddressGroup.MapDelete("/{addressId:int}",
            async (int addressId, ICustomerAddressService customerAddressService) =>
            {
                await customerAddressService.RemoveAddress(addressId);

                return Results.Ok();
            }
        );

        return endpoints;
    }
}