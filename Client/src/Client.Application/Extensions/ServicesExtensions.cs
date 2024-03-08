using Microsoft.Extensions.DependencyInjection;
using SunRaysMarket.Client.Application.State;
using SunRaysMarket.Shared.Extensions.Reflection;
using SunRaysMarket.Shared.Services;

namespace SunRaysMarket.Client.Application.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddClientOnlyApplicationServices(
        this IServiceCollection services
    )
    {
        var interfaceNamespaceDescriptors = new[]
        {
            new NamespaceDescriptor(
                typeof(ISharedServicesMarker).Assembly,
                "SunRaysMarket.Shared.Services.Interfaces"
            )
        };

        services.AddInterfacesWithImplementationsFromLocalNamespace(
            interfaceNamespaceDescriptors,
            "SunRaysMarket.Client.Application.ProxyServicesImpl"
        );

        services.AddSingleton<ProductModalState>();

        return services;
    }
}
