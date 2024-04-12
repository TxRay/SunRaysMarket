using Microsoft.Extensions.DependencyInjection;
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
        var interfaceNamespaceDescriptors = NamespaceDescriptor.FromTypes(typeof(IAddressService));

        services.AddInterfacesWithImplementationsFromLocalNamespace(
            interfaceNamespaceDescriptors,
            "SunRaysMarket.Client.Application.ProxyServicesImpl"
        );

        services.AddSingleton<ProductModalState>();

        return services;
    }
}