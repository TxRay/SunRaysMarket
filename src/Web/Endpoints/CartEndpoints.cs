using System.Security.Claims;
using Application.Auth;
using Application.Services;
using Application.UnitOfWork;
using Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Web.Cookies;

namespace Web.Endpoints;

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
            async (
                HttpContext context,
                ICustomerService customerService,
                IUserService userService,
                IUnitOfWork unitOfWork
            ) =>
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

                return Results.Json(new { CartId = cartId });
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
            "/add-item/{productId}",
            async (
                int productId,
                [FromQuery] int quantity,
                HttpContext context,
                ICustomerService customerService,
                IUnitOfWork unitOfWork
            ) =>
            {
                var customerId = await customerService.GetCurrentCustomerIdAsync(context.User);
                var cartId = customerId is not null
                    ? await customerService.GetCustomerCartIdAsync(customerId.Value)
                    : context.Request.Cookies.GetCartIdCookie();

                if (cartId is null)
                    return Results.BadRequest("Cart not found.");

                await unitOfWork.CartRepository.AddProductToCartAsync(
                    cartId.Value,
                    productId,
                    quantity,
                    true
                );
                await unitOfWork.SaveChangesAsync();

                var cartItemId = unitOfWork.CartRepository.GetPersistedCartItemId();

                return Results.Json(new { CartItemId = cartItemId });
            }
        );

        endpoints.MapPost(
            "/remove-item/by-id/{cartItemId}",
            async (int cartItemId, IUnitOfWork unitOfWork) =>
            {
                await unitOfWork.CartRepository.RemoveItemFromCartAsync(cartItemId);
                await unitOfWork.SaveChangesAsync();

                return Results.Ok();
            }
        );

        endpoints.MapPost(
            "/remove-item/by-product-id/{productId}",
            async (
                int productId,
                HttpContext context,
                ICustomerService customerService,
                IUnitOfWork unitOfWork
            ) =>
            {
                var cartId = context.User.IsAuthenticated()
                    ? await customerService.GetCustomerCartIdAsync(context.User)
                    : context.Request.Cookies.GetCartIdCookie();

                if (cartId is null)
                    return Results.BadRequest("Cart not found.");

                await unitOfWork.CartRepository.RemoveItemFromCartAsync(cartId.Value, productId);
                await unitOfWork.SaveChangesAsync();

                return Results.Ok();
            }
        );

        endpoints.MapPost(
            "/update-item-quantity/{cartItemId}",
            async (int cartItemId, [FromQuery] int quantity, IUnitOfWork unitOfWork) =>
            {
                await unitOfWork.CartRepository.UpdateProductQuantityAsync(cartItemId, quantity);
                await unitOfWork.SaveChangesAsync();

                return Results.Ok();
            }
        );

        return endpoints;
    }
}
