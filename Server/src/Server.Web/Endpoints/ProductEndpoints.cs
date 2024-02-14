using Microsoft.AspNetCore.Mvc;
using SunRaysMarket.Shared.Core.DomainModels.Responses;

namespace SunRaysMarket.Server.Web.Endpoints;

internal static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var productGroup = endpoints.MapGroup("/product");
        var allProductsGroup = endpoints.MapGroup("/products");

        productGroup.MapGet(
            "/{id:int}",
            async (int id, IProductService productService) =>
                Results.Json(await productService.GetProductDetailsAsync(id))
        );

        allProductsGroup.MapGet("/", GetFeatureProductsAsync);
        allProductsGroup.MapGet("/{departmentId:int}", GetDepartmentFeaturedProductsAsync);

        allProductsGroup.MapPost(
            "/search",
            async ([FromBody] ProductSearchCommand searchCommand, IProductService productService) =>
                Results.Json(
                    new GetProductListResponse
                    {
                        Products = await productService.SearchForProductsAsync(searchCommand.Query)
                    }
                )
        );

        return endpoints;
    }

    private static async IAsyncEnumerable<IResult> GetFeatureProductsAsync(
        IProductService productService
    )
    {
        await foreach (var product in productService.GetAllProductsAsync())
        {
            yield return Results.Json(product);
        }
    }

    private static async IAsyncEnumerable<IResult> GetDepartmentFeaturedProductsAsync(
        int departmentId,
        IProductService productService
    )
    {
        await foreach (var product in productService.GetAllProductsAsync(departmentId))
        {
            yield return Results.Json(product);
        }
    }
}
