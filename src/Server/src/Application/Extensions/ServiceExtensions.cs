using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SunRaysMarket.Server.Application.ServicesImpl.Scoped.Auth;
using SunRaysMarket.Server.Core.Services;
using SunRaysMarket.Shared.Core.Services;
using SunRaysMarket.Shared.Extensions.Reflection;

namespace SunRaysMarket.Server.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServerApplicationAssembly(this IServiceCollection services)
    {
        var interfaceNamespaceDescriptors = NamespaceDescriptor.FromTypes(
            typeof(IOrderService),
            typeof(IAddressService)
        );
        

        services.AddInterfacesWithImplementationsFromLocalNamespace(
            interfaceNamespaceDescriptors,
            "SunRaysMarket.Server.Application.ServicesImpl"
        );

        services.AddValidatorsFromAssemblyContaining<SignUpService>();

        return services;
    }
}