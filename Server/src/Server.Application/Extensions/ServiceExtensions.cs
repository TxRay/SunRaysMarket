using System.Reflection;
using System.Text.RegularExpressions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Extensions;
using SunRaysMarket.Server.Application.Services;
using SunRaysMarket.Server.Application.Services.Auth;
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

        services.AddSInterfacesWithImplementationsFromLocalNamespace(
            interfaceNamespaceDescriptors,
            "SunRaysMarket.Server.Application.Services",
            ServiceLifetime.Scoped
        );

        services.AddValidatorsFromAssemblyContaining<SignUpService>();

        return services;
    }
}
