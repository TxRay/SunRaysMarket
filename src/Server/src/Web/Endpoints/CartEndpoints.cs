using Microsoft.AspNetCore.Mvc;
using SunRaysMarket.Server.Application.Exceptions;
using SunRaysMarket.Server.Core.Services;
using SunRaysMarket.Shared.Core.DomainModels.Responses;
using SunRaysMarket.Shared.Core.Services;

namespace SunRaysMarket.Server.Web.Endpoints;

internal static class CartEndpoints
{
    public static IEndpointRouteBuilder MapCartEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var cartGroup = endpoints
            .MapGroup("/cart")
            .WithGroupName("Cart")
            .WithDescription("Endpoints for managing shopping carts.");

        cartGroup.MapDelete("/", DeleteCartHandler);
        cartGroup
            .MapGet("/items/{cartId:int}", GetCartItemsHandler)
            .Produces<IEnumerable<CartItemListModel>>();
        cartGroup
            .MapGet("/items", GetActiveCartItemsHandler)
            .Produces<IEnumerable<CartItemListModel>>();
        cartGroup
            .MapGet("/items/streamed", GetActiveCartItemsEnumerableHandler)
            .Produces<IAsyncEnumerable<CartItemListModel>>();

        cartGroup.MapCreateCartEndpoint();
        //cartGroup.MapGetCartEndpoint();
        cartGroup.MapAddCartItemEndpoints();
        cartGroup.MapCartItemInfoEndpoints();

        return endpoints;
    }

    private static async Task<IResult> GetCartItemsHandler(int cartId, ICartService cartService)
    {
        return Results.Json(await cartService.GetCartItemsAsync(cartId));
    }

    private static async Task<IResult> GetActiveCartItemsHandler(ICartService cartService)
    {
        return Results.Json(await cartService.GetActiveCartItemsAsync());
    }

    private static async Task<IResult> DeleteCartHandler(ICartControlsService cartControlsService)
    {
        await cartControlsService.DeleteCartAsync();
        return Results.Ok();
    }

    private static async IAsyncEnumerable<IResult> GetActiveCartItemsEnumerableHandler(
        ICartService cartService
    )
    {
        await foreach (var item in cartService.GetActiveCartItemsAsyncEnumerable())
            yield return Results.Json(item);
    }

    private static IEndpointRouteBuilder MapCreateCartEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
            "/create",
            async (ICartControlsService cartService) =>
                Results.Json(await cartService.CreateCartAsync())
        );

        return endpoints;
    }

    /*private static IEndpointRouteBuilder MapGetCartEndpoint(this IEndpointRouteBuilder endpoints)
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
    }*/

    private static IEndpointRouteBuilder UpdateCareEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/update", async () => { });

        return endpoints;
    }

    private static IEndpointRouteBuilder MapAddCartItemEndpoints(
        this IEndpointRouteBuilder endpoints
    )
    {
        endpoints.MapPost(
            "/add-item",
            async (
                [FromBody] AddItemToCartCommand command,
                HttpContext context,
                ICookieService cookieService,
                ICartControlsService cartService
            ) =>
            {
                var cartId = cookieService.CartId ?? (await cartService.CreateCartAsync()).CartId;

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
            ([FromBody] RemoveCartItemCommand command, ICartControlsService cartControlsService) =>
                cartControlsService.RemoveItemAsync(command)
        );

        endpoints.MapPost(
            "/update-item-quantity",
            async (
                [FromBody] UpdateCartItemQuantityCommand command,
                ICartControlsService cartControlsService
            ) => Results.Json(await cartControlsService.UpdateQuantityAsync(command))
        );

        return endpoints;
    }

    private static void MapCartItemInfoEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var itemInfoGroup = endpoints.MapGroup("/item-info");

        itemInfoGroup.MapGet(
            "/{itemId:int}",
            async (int itemId, ICartControlsService cartService) =>
                Results.Json(await cartService.GetCartItemInfoAsync(itemId))
        );

        itemInfoGroup
            .MapGet(
                "/",
                async (ICartControlsService cartService) =>
                    Results.Json(
                        new GetCartItemInfoListResponse
                        {
                            CartItemInfoList = await cartService.GetAllCartItemInfoAsync()
                        }
                    )
            )
            .Produces<IEnumerable<CartItemControlModel>>();
    }
}
