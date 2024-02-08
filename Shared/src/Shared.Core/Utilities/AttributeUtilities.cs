using System.Diagnostics.CodeAnalysis;

namespace SunRaysMarket.Shared.Core.Utilities;

public static class AttributeUtilities
{
    public static Dictionary<string, object> ConvertToDictionary(
        IReadOnlyDictionary<string, object>? additionalAttributes,
        IReadOnlyDictionary<string, IAttributeObject>? defaultAttributes
    )
    {
        var attributesDictionary = additionalAttributes switch
        {
            null => new Dictionary<string, object>(),
            Dictionary<string, object> dict => dict,
            _ => additionalAttributes.ToDictionary()
        };

        if (defaultAttributes is not null)
        {
            foreach (var (key, attributeObject) in defaultAttributes)
            {
                if (!attributesDictionary.TryAdd(key, attributeObject.CombineWithDefault(null)))
                    attributesDictionary[key] = attributeObject.CombineWithDefault(
                        attributesDictionary[key]
                    );
            }
        }

        return attributesDictionary;
    }

    public interface IAttributeObject
    {
        public object Value { get; }
        public object CombineWithDefault(object? additionalValue);
    }

    public record AttributeObject<TValue>(
        [NotNull] TValue ValueTyped,
        Func<TValue, TValue, object>? RenderAsType = null
    ) : IAttributeObject
    {
        public object Value => ValueTyped!;

        public object CombineWithDefault(object? additionalValue)
        {
            var additionalValueTyped = additionalValue switch
            {
                TValue val => val,
                _ => default
            };

            if (additionalValueTyped is null)
                return ValueTyped!;

            return (RenderAsType is null)
                ? additionalValueTyped
                : RenderAsType.Invoke(ValueTyped, additionalValueTyped);
        }
    }
}
