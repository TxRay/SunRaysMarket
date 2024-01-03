using Application.EndpointViewModels;
using Application.Services;

namespace WebBlazor.Endpoints;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var productGroup = endpoints.MapGroup("/product");
        var allProductsGroup = endpoints.MapGroup("/products");

        productGroup.MapGet("/{id:int}", async (int id, IProductService productService)
            => Results.Json(await productService.GetProductDetailsAsync(id))
        );

        allProductsGroup.MapGet("/",
            async (IProductService productService) =>
                Results.Json(
                    new GetProductListResponse { Products = await productService.GetAllProductsAsync() }
                )
        );

        allProductsGroup.MapGet("/{departmentId:int}",
            async (int departmentId, IProductService productService)
                => Results.Json(
                    new GetProductListResponse
                    {
                        Products = await productService.GetAllProductsAsync(departmentId)
                    }
                )
        );

        return endpoints;
    }
}