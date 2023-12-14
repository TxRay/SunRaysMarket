using Application.Cookies;
using Application.EndpointViewModels;
using Application.Exceptions;
using Application.Services;
using Application.UnitOfWork;
using Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebBlazor.Endpoints;

public static class CartEndpoints
{
    public static IEndpointRouteBuilder MapCartEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var cartGroup = endpoints
            .MapGroup("/cart")
            .WithGroupName("Cart")
            .WithDescription("Endpoints for managing shopping carts.");

        cartGroup.MapCreateCartEndpoint();
        cartGroup.MapGetCartEndpoint();
        cartGroup.MapAddCartItemEndpoints();

        return endpoints;
    }

    public static IEndpointRouteBuilder MapCreateCartEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
            "/create",
            async (HttpContext context, ICustomerService customerService, IUnitOfWork unitOfWork) =>
            {
                if (context.User.IsAuthenticated())
                    await customerService.CreateCustomerCartAsync(context.User);
                else
                {
                    await unitOfWork.CartRepository.CreateCartAsync(null, true);
                    await unitOfWork.SaveChangesAsync();
                }

                var cartId = unitOfWork.CartRepository.GetPersistedCartId();
                context.Response.Cookies.SetCartIdCookie(cartId);

                return Results.Json(new CreateCartResponse { CartId = cartId });
            }
        );

        return endpoints;
    }

    private static IEndpointRouteBuilder MapGetCartEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/get-id",
            async (HttpContext context, ICustomerService customerService) =>
            {
                var cartId = context.User.IsAuthenticated()
                    ? await customerService.GetCustomerCartIdAsync(context.User)
                    : context.Request.Cookies.GetCartIdCookie();

                return cartId is not null
                    ? Results.Json(new { CartId = cartId })
                    : Results.NotFound("Cart not found.");
            }
        );

        endpoints.MapGet(
            "/exists",
            (HttpContext context) =>
                Results.Json(
                    new CheckCartExistsResponse
                    {
                        CartExists = context.Request.Cookies.GetCartIdCookie() is not null
                    }
                )
        );

        return endpoints;
    }

    public static IEndpointRouteBuilder UpdateCareEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/update", async () => { });

        return endpoints;
    }

    public static IEndpointRouteBuilder MapAddCartItemEndpoints(
        this IEndpointRouteBuilder endpoints
    )
    {
        endpoints.MapPost(
            "/add-item",
            async (
                [FromBody] AddItemToCartCommand command,
                HttpContext context,
                ICartControlsService cartControlsService
            ) =>
            {
                var cartId = context.Request.Cookies.GetCartIdCookie();

                if (cartId is null)
                    return Results.BadRequest("Cart not found.");

                try
                {
                    return Results.Json(
                        await cartControlsService.AddItemToCartAsync(
                            builder =>
                            {
                                builder.WithCartId(cartId.Value);
                                builder.WithCommand(command);
                            }
                        )
                    );
                }
                catch (AddItemFailedException exc)
                {
                    return Results.BadRequest(exc.Message);
                }
            }
        );

        endpoints.MapPost(
            "/remove-item",
            ([FromBody] RemoveCartItemCommand command, ICartControlsService cartControlsService) =>
                cartControlsService.RemoveItemAsync(command)
        );

        endpoints.MapPost(
            "/update-item-quantity",
            async ([FromBody] UpdateCartItemQuantityCommand command, ICartControlsService cartControlsService) =>
            Results.Json(await cartControlsService.UpdateQuantityAsync(command))
        );

        return endpoints;
    }
}