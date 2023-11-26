using Application.DomainModels;
using Application.UnitOfWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        componentGroup.MapCartControlEndpoints();
        componentGroup.MapSignUpComponentEndpoints();

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

    private static IEndpointRouteBuilder MapCartControlEndpoints(
        this IEndpointRouteBuilder endpoints
    )
    {
        endpoints.MapPost(
            "/add-to-cart",
            () => new RazorComponentResult<CartControls>(new { Quantity = 1 })
        );

        endpoints.MapPost(
            "/quantity-control/{customerId:int}/{productId:int}/{idSuffix}",
            (int customerId, int productId, string? idSuffix, [FromForm] int quantity) =>
                new RazorComponentResult<QuantityControl>(new {Quantity=quantity, IdSuffix = idSuffix})
            
        );

        return endpoints;
    }
    
    private static IEndpointRouteBuilder MapSignUpComponentEndpoints(
        this IEndpointRouteBuilder endpoints
    )
    {
        endpoints.MapGet(
            "/sign-up",
            () => new RazorComponentResult<SignUp>()
        );

        endpoints.MapPost(
            "/sign-up",
            (SignupModel model) => new RazorComponentResult<SignUp>()
            );

        return endpoints;
    }
}
