using Microsoft.Extensions.DependencyInjection;
using SunRaysMarket.Client.Application.ProxyServicesImpl.Scoped;
using SunRaysMarket.Client.Application.State;
using SunRaysMarket.Shared.Core.Services;
using SunRaysMarket.Shared.Extensions.Reflection;

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
                typeof(IAddressService).Assembly,
                typeof(IAddressService).Namespace!
            )
        };

        services.AddInterfacesWithImplementationsFromLocalNamespace(
            interfaceNamespaceDescriptors,
            "SunRaysMarket.Client.ProxyServicesImpl"
        );

        services.AddSingleton<ProductModalState>();

        return services;
    }
}
