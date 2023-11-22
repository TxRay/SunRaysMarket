using Application.DomainModels;
using Application.UnitOfWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Web.Components;

namespace Web.Endpoints;

public static class ComponentEndpoints
{
    public static IEndpointRouteBuilder MapComponentEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var componentGroup = endpoints
            .MapGroup("/Components")
            .WithGroupName("Components")
            .WithDescription("Endpoints which return HTML rendered by Blazor components.");

        componentGroup.MapProductDetailsComponentEndpoint();

        return endpoints;
    }

    private static IEndpointRouteBuilder MapProductDetailsComponentEndpoint(
        this IEndpointRouteBuilder endpoints
    )
    {
        endpoints.MapGet(
            "/product-details/{id:int}",
            async (int id, IUnitOfWork unitOfWork) =>
            {
                var product = await unitOfWork.ProductRepository.GetAsync(id);

                return new RazorComponentResult<ProductDetails>(new { Product = product });
            }
        );

        return endpoints;
    }
}
