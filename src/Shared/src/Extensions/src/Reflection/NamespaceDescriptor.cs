using System.Reflection;

namespace SunRaysMarket.Shared.Extensions.Reflection;

/// <summary>
///     Describes a namespace in an assembly
/// </summary>
/// <param name="Assembly">The assembly containing the namespace</param>
/// <param name="NameSpace">The namespace to be described</param>
public record NamespaceDescriptor(Assembly Assembly, string NameSpace)
{
    public static NamespaceDescriptor FromType(Type type)
        => new NamespaceDescriptor(type.Assembly, type.Namespace!);
    
    public static IEnumerable<NamespaceDescriptor> FromTypes(params Type[] types)
        => types.Select(FromType);
}