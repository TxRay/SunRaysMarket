using Application.Auth;
using Application.DomainModels;
using Application.UnitOfWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Web;
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
        componentGroup.MapAuthComponentEndpoints();
        componentGroup.MapShoppingCartEndpoints();

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
                new RazorComponentResult<QuantityControl>(
                    new { Quantity = quantity, IdSuffix = idSuffix }
                )
        );

        return endpoints;
    }

    private static IEndpointRouteBuilder MapAuthComponentEndpoints(
        this IEndpointRouteBuilder endpoints
    )
    {
        endpoints.MapGet("/sign-up", () => new RazorComponentResult<SignUp>());

        endpoints.MapPost(
            "/sign-up",
            ([FromForm] SignUpModel model, HttpContext context, ISignUpService signUpService) =>
            {
                var result = signUpService.CustomerSignUpAsync(model, new[] { Role.Customer });

                return result.Result.WasSuccessful
                    ? Results.Ok($"<p>{result.Result.Message}</p>")
                    : new RazorComponentResult<SignUp>();
            }
        );

        endpoints.MapGet("/login", (HttpContext context) =>
        {
            var loginEmail = context.Session.GetString("LoginEmail") ?? string.Empty;
            var model = new LoginModel { Email = loginEmail };
            
            return new RazorComponentResult<Login>(new {LoginModel = model });
        });

        endpoints.MapPost(
            "/login",
            async ([FromForm] LoginModel model, HttpContext context, ILoginService loginService) =>
            {
                var result = await loginService.LoginAsync(model);
                

                return new RazorComponentResult<Login>(
                    new { ShowConfirmation = result.WasSuccessful }
                );
            }
        );

        endpoints.MapPost(
            "/logout",
            async (HttpContext context, IUserService userService) =>
            {
                await userService.LogoutAsync();

                return new RazorComponentResult<NavAccount>(new { IsAuthenticated = false });
            }
        );

        endpoints.MapGet(
            "/nav-account",
            (HttpContext context) =>
            {
                var isAuthenticated = context.User.Identity?.IsAuthenticated ?? false;

                return new RazorComponentResult<NavAccount>(
                    new { IsAuthenticated = isAuthenticated }
                );
            }
        );

        return endpoints;
    }

    private static IEndpointRouteBuilder MapShoppingCartEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/shopping-cart", () => new RazorComponentResult<ShoppingCart>());
        
        return endpoints;
    }
}
