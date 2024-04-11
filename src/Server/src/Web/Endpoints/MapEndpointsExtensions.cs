using System.Reflection;

namespace SunRaysMarket.Server.Web.Endpoints;

public static class MapEndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var apiGroup = endpoints
            .MapGroup("/api")
            .WithGroupName("Api")
            .WithDescription("Api endpoints");

        var mappingMethods = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(
                type =>
                    type is { Namespace: "SunRaysMarket.Server.Web.Endpoints", IsClass: true }
                    && type.FullName != "SunRaysMarket.Server.Web.Endpoints.MapEndpointsExtensions"
            )
            .SelectMany(
                type =>
                    type.GetMethods().Where(method => method is { IsPublic: true, IsStatic: true })
            );

        foreach (var method in mappingMethods)
            method.Invoke(null, [apiGroup]);

        return endpoints;
    }
}
