using Application.EndpointViewModels;
using Application.Services;
using Application.UnitOfWork;
using Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Cookies;

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
                ICustomerService customerService,
                IUnitOfWork unitOfWork
            ) =>
            {
                var cartId = context.Request.Cookies.GetCartIdCookie();

                if (cartId is null)
                    return Results.BadRequest("Cart not found.");

                await unitOfWork
                    .CartRepository
                    .AddProductToCartAsync(cartId.Value, command.ProductId, command.Quantity, true);

                try
                {
                    await unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    return Results.BadRequest(
                        "Multiple items of the same product are not allowed in the same cart."
                    );
                }

                var cartItemId = unitOfWork.CartRepository.GetPersistedCartItemId();

                return Results.Json(new AddItemToCartResponse { ItemId = cartItemId });
            }
        );

        endpoints.MapPost(
            "/remove-item",
            async ([FromBody] RemoveCartItemCommand command, IUnitOfWork unitOfWork) =>
            {
                await unitOfWork.CartRepository.RemoveItemFromCartAsync(command.ItemId);
                await unitOfWork.SaveChangesAsync();

                return Results.Ok();
            }
        );

        endpoints.MapPost(
            "/update-item-quantity",
            async ([FromBody] UpdateCartItemQuantityCommand command, IUnitOfWork unitOfWork) =>
            {
                await unitOfWork
                    .CartRepository
                    .UpdateProductQuantityAsync(command.CartItemId, command.NewQuantity, true);
                await unitOfWork.SaveChangesAsync();

                var persistedQuantity = unitOfWork.CartRepository.GetPersistedCartItemQuantity();

                return Results.Json(
                    new UpdateCartItemQuantityResponse
                    {
                        UpdatedQuantity =
                            persistedQuantity is null || persistedQuantity != command.NewQuantity
                                ? command.OldQuantity
                                : persistedQuantity.Value
                    }
                );
            }
        );

        return endpoints;
    }
}
