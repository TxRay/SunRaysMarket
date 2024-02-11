using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shared.Extensions;
using SunRaysMarket.Client.Application.ProxyServices;
using SunRaysMarket.Client.Application.State;
using SunRaysMarket.Shared.Services;
using SunRaysMarket.Shared.Services.Interfaces;

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
            "SunRaysMarket.Client.Application.ProxyServices",
            ServiceLifetime.Scoped
        );

        services.AddSingleton<ProductModalState>();

        return services;
    }
}