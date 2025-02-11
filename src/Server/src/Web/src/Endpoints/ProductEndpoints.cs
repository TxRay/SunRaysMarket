using Microsoft.AspNetCore.Mvc;
using SunRaysMarket.Server.Core.Services;
using SunRaysMarket.Shared.Core.DomainModels.Responses;
using SunRaysMarket.Shared.Core.Services;

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
        allProductsGroup.MapPost("/search", ProductSearchHandler);

        return endpoints;
    }

    private static IAsyncEnumerable<IResult> ProductSearchHandler(
        [FromBody] ProductSearchCommand searchCommand,
        IProductSearchService productSearchService
    )
    {
        return productSearchService
            .GetSearchResults(searchCommand.Query)
            .Select(product => Results.Json(product));
    }

    private static IAsyncEnumerable<IResult> GetFeatureProductsAsync(IProductService productService)
    {
        return productService.GetAllProductsAsync().Select(product => Results.Json(product));
    }

    private static IAsyncEnumerable<IResult> GetDepartmentFeaturedProductsAsync(
        int departmentId,
        IProductService productService
    )
    {
        return productService
            .GetAllProductsAsync(departmentId)
            .Select(product => Results.Json(product));
    }
}