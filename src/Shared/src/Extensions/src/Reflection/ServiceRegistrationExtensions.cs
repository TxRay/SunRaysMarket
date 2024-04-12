using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;

namespace SunRaysMarket.Shared.Extensions.Reflection;

/// <summary>
///     Provides extension methods for the <see cref="IServiceCollection" /> interface to register services
///     from a given namespace in an assembly.
/// </summary>
public static class ServiceRegistrationExtensions
{
    private static readonly Regex Alphanumeric = new("^[a-zA-Z0-9]+$");

    /// <summary>
    ///     Registers all classes from a given namespace in an assembly as services in the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">
    ///     The <see cref="IServiceCollection" /> to register the services in.
    /// </param>
    /// <param name="assembly">
    ///     The assembly containing the namespace.
    /// </param>
    /// <param name="nameSpace">The namespace to register the services from.</param>
    /// <param name="lifetime">The lifetime of the registered services.</param>
    /// <returns>The <see cref="IServiceCollection" /> with the services registered.</returns>
    public static IServiceCollection AddImplementationsFromNamespace(
        this IServiceCollection services,
        Assembly assembly,
        string nameSpace,
        ServiceLifetime? lifetime = null
    )
    {
        var serviceDescriptors = assembly
            .GetTypes()
            .Where(
                type =>
                    (type.Namespace?.StartsWith(nameSpace) ?? false)
                    && type.IsClass
                    && Alphanumeric.IsMatch(type.Name)
            )
            .Select(
                type =>
                    new ServiceDescriptor(
                        type,
                        serviceProvider => ActivatorUtilities.CreateInstance(serviceProvider, type),
                        lifetime ?? MapServiceLifetime(nameSpace, type.Namespace!)
                    )
            );

        foreach (var descriptor in serviceDescriptors)
            services.Add(descriptor);

        return services;
    }

    /// <summary>
    ///     Registers all classes from a given namespace in an assembly as services in the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">
    ///     The <see cref="IServiceCollection" /> to register the services in.
    /// </param>
    /// <param name="namespaceDescriptor">
    ///     The <see cref="NamespaceDescriptor" /> containing the assembly and the namespace to register the services from.
    /// </param>
    /// <param name="lifetime">
    ///     The lifetime of the registered services.
    /// </param>
    /// <returns></returns>
    public static IServiceCollection AddImplementationsFromNamespace(
        this IServiceCollection services,
        NamespaceDescriptor namespaceDescriptor,
        ServiceLifetime? lifetime
    )
    {
        return services.AddImplementationsFromNamespace(
            namespaceDescriptor.Assembly,
            namespaceDescriptor.NameSpace,
            lifetime = null
        );
    }

    /// <summary>
    ///     Registers all classes from a given namespace in the calling assembly as services in the
    ///     <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">
    ///     The <see cref="IServiceCollection" /> to register the services in.
    /// </param>
    /// <param name="nameSpace">
    ///     The namespace to register the services from.
    /// </param>
    /// <param name="lifetime">
    ///     The lifetime of the registered services.
    /// </param>
    /// <returns>
    ///     The <see cref="IServiceCollection" /> with the services registered.
    /// </returns>
    public static IServiceCollection AddImplementationsFromLocalNameSpace(
        this IServiceCollection services,
        string nameSpace,
        ServiceLifetime? lifetime = null
    )
    {
        return services.AddImplementationsFromNamespace(
            Assembly.GetCallingAssembly(),
            nameSpace,
            lifetime
        );
    }

    /// <summary>
    ///     Registers all classes from a given namespace in an assembly as services in the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">
    ///     The <see cref="IServiceCollection" /> to register the services in.
    /// </param>
    /// <param name="interfaceNamespaces">
    ///     The namespaces containing the interfaces to be implemented.
    /// </param>
    /// <param name="implementationAssembly">
    ///     The assembly containing the implementation classes.
    /// </param>
    /// <param name="implementationNameSpace">
    ///     The namespace containing the implementation classes.
    /// </param>
    /// <param name="lifetime">
    ///     The lifetime of the registered services.
    /// </param>
    /// <returns>
    ///     The <see cref="IServiceCollection" /> with the services registered.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when a service does not implement an interface from the given namespaces.
    /// </exception>
    public static IServiceCollection AddInterfacesWithImplementationsFromNamespace(
        this IServiceCollection services,
        IEnumerable<NamespaceDescriptor> interfaceNamespaces,
        Assembly implementationAssembly,
        string implementationNameSpace,
        ServiceLifetime? lifetime = null
    )
    {
        var implementationTypes = implementationAssembly
            .GetTypes()
            .Where(
                type =>
                    (type.Namespace?.StartsWith(implementationNameSpace) ?? false)
                    && type.IsClass
                    && Alphanumeric.IsMatch(type.Name)
            );

        var interfaceTypes = interfaceNamespaces.SelectMany(
            nsd =>
                nsd.Assembly
                    .GetTypes()
                    .Where(
                        type =>
                            (type.Namespace?.StartsWith(nsd.NameSpace) ?? false)
                            && type.IsInterface
                            && Alphanumeric.IsMatch(type.Name)
                    )
        );

        foreach (var implType in implementationTypes)
        {
            var interfaceType =
                interfaceTypes.FirstOrDefault(type => type.IsAssignableFrom(implType))
                ?? throw new InvalidOperationException(
                    $"The service '{implType.FullName}' does not implement an interface "
                    + $"from the given namespaces."
                );

            var lifetimeValue =
                lifetime ?? MapServiceLifetime(implementationNameSpace, implType.Namespace!);

            var serviceDescriptor = new ServiceDescriptor(
                interfaceType,
                serviceProvider => ActivatorUtilities.CreateInstance(serviceProvider, implType),
                lifetimeValue
            );

            services.Add(serviceDescriptor);
        }

        return services;
    }

    /// <summary>
    ///     Registers all classes from a given namespace in an assembly as services in the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">
    ///     The <see cref="IServiceCollection" /> to register the services in.
    /// </param>
    /// <param name="interfaceNamespaces">
    ///     The namespaces containing the interfaces to be implemented.
    /// </param>
    /// <param name="implementationNamespaceDescriptor">
    ///     The <see cref="NamespaceDescriptor" /> containing the assembly and the namespace to register the services from.
    /// </param>
    /// <param name="lifetime">
    ///     The lifetime of the registered services.
    /// </param>
    /// <returns>
    ///     The <see cref="IServiceCollection" /> with the services registered.
    /// </returns>
    public static IServiceCollection AddInterfacesWithImplementationsFromNamespace(
        this IServiceCollection services,
        IEnumerable<NamespaceDescriptor> interfaceNamespaces,
        NamespaceDescriptor implementationNamespaceDescriptor,
        ServiceLifetime? lifetime = null
    )
    {
        return services.AddInterfacesWithImplementationsFromNamespace(
            interfaceNamespaces,
            implementationNamespaceDescriptor.Assembly,
            implementationNamespaceDescriptor.NameSpace,
            lifetime
        );
    }

    /// <summary>
    ///     Registers all classes from a given namespace in the calling assembly as services in the
    ///     <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">
    ///     The <see cref="IServiceCollection" /> to register the services in.
    /// </param>
    /// <param name="interfaceNamespaces">
    ///     The namespaces containing the interfaces to be implemented.
    /// </param>
    /// <param name="implementationNamespace">
    ///     The namespace to register the services from.
    /// </param>
    /// <param name="lifetime">
    ///     The lifetime of the registered services.
    /// </param>
    /// <returns>
    ///     The <see cref="IServiceCollection" /> with the services registered.
    /// </returns>
    public static IServiceCollection AddInterfacesWithImplementationsFromLocalNamespace(
        this IServiceCollection services,
        IEnumerable<NamespaceDescriptor> interfaceNamespaces,
        string implementationNamespace,
        ServiceLifetime? lifetime = null
    )
    {
        return services.AddInterfacesWithImplementationsFromNamespace(
            interfaceNamespaces,
            Assembly.GetCallingAssembly(),
            implementationNamespace,
            lifetime
        );
    }

    private static ServiceLifetime MapServiceLifetime(string baseNamespace, string serviceNamespace)
    {
        if (serviceNamespace.StartsWith($"{baseNamespace}.Transient"))
            return ServiceLifetime.Transient;

        if (serviceNamespace.StartsWith($"{baseNamespace}.Scoped"))
            return ServiceLifetime.Scoped;

        if (serviceNamespace.StartsWith($"{baseNamespace}.Singleton"))
            return ServiceLifetime.Singleton;

        throw new InvalidOperationException(
            $"No service lifetime was indicated by the given namespace {serviceNamespace}."
        );
    }
}