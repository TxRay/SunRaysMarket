using Application.Cookies;
using Application.EndpointViewModels;
using Application.Exceptions;
using Application.Services;
using Application.Utilities;
using Microsoft.AspNetCore.Mvc;

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
            async (ICartService cartService) => Results.Json(await cartService.CreateCartAsync())
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
                ICartService cartService
            ) =>
            {
                var cartId = context.Request.Cookies.GetCartIdCookie()
                    ?? (await cartService.CreateCartAsync()).CartId;
                
                try
                {
                    return Results.Json(
                        await cartService.AddItemToCartAsync(builder =>
                        {
                            builder.WithCartId(cartId);
                            builder.WithCommand(command);
                        })
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
            ([FromBody] RemoveCartItemCommand command, ICartService cartControlsService) =>
                cartControlsService.RemoveItemAsync(command)
        );

        endpoints.MapPost(
            "/update-item-quantity",
            async (
                [FromBody] UpdateCartItemQuantityCommand command,
                ICartService cartControlsService
            ) => Results.Json(await cartControlsService.UpdateQuantityAsync(command))
        );

        return endpoints;
    }

    private static void MapCartItemInfoEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var itemInfoGroup = endpoints.MapGroup("/item-info");

        itemInfoGroup.MapGet("/{itemId:int}",
            async (int itemId, ICartService cartService)
                => Results.Json(
                    await cartService.GetCartItemInfoAsync(itemId)
                )
        );

        itemInfoGroup.MapGet("/",
            async (ICartService cartService)
                => Results.Json(
                    new GetCartItemInfoListResponse
                    {
                        CartItemInfoList = await cartService.GetAllCartItemInfoAsync()
                    }
                )
        );
    }
}