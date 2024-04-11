using SunRaysMarket.Server.Core.DomainModels;

namespace SunRaysMarket.Server.Infrastructure.Data.PersistenceModels.Extensions;

internal static class CustomerMappingExtensions
{
    public static Customer UpdateEntity(
        this UpdateCustomerPreferencesModel domainModel,
        Customer entity
    )
    {
        foreach (var propertyInfo in domainModel.GetType().GetProperties())
        {
            var entityPropertyInfo =
                entity.GetType().GetProperty(propertyInfo.Name)
                ?? throw new InvalidOperationException(
                    $"$The entity {typeof(Customer).FullName} does not contain "
                        + $"a property named '{propertyInfo}'."
                );
            object? value;
            if ((value = propertyInfo.GetValue(domainModel)) != entityPropertyInfo.GetValue(entity))
                entityPropertyInfo.SetValue(entity, value);
        }

        return entity;
    }
}
