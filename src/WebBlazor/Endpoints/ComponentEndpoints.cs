using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.FileProviders;
using WebClient.Components;

namespace WebBlazor.Endpoints;

public static class ComponentEndpoints
{
    
    public static IEndpointRouteBuilder MapComponentEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var componentsGroup = endpoints.MapGroup("/components")
            .WithName("Components")
            .WithDescription("Api endpoints to server Blazor components for AJAX requests.");
        
        //componentsGroup.MapGet("/modal-outlet", () => new RazorComponentResult<ModalOutlet>())
         //   .WithName("ModalOutlet");

        componentsGroup.MapGet("/product-details/{id:int}",
            (int id) => new RazorComponentResult<ProductDetails>(new {ProductId = id}))
            .WithName("ProductDetails");
        
        return endpoints;
    }
    
}