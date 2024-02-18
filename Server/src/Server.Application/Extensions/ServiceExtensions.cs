using System.Reflection;
using System.Text.RegularExpressions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SunRaysMarket.Server.Application.Services;
using SunRaysMarket.Server.Application.Services.Auth;
using SunRaysMarket.Server.Application.ServicesImpl.Scoped.Auth;
using SunRaysMarket.Shared.Extensions.Reflection;
using SunRaysMarket.Shared.Services;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Server.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServerApplicationAssembly(this IServiceCollection services)
    {
        var interfaceNamespaceDescriptors = new[]
        {
            new NamespaceDescriptor(
                Assembly.GetExecutingAssembly(),
                "SunRaysMarket.Server.Application.Services"
            ),
            new NamespaceDescriptor(
                typeof(ISharedServicesMarker).Assembly,
                "SunRaysMarket.Shared.Services.Interfaces"
            )
        };

        services.AddInterfacesWithImplementationsFromLocalNamespace(
            interfaceNamespaceDescriptors,
            "SunRaysMarket.Server.Application.ServicesImpl"
        );

        services.AddValidatorsFromAssemblyContaining<SignUpService>();

        return services;
    }
}