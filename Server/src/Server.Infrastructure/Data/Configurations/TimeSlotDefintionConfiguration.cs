using SunRaysMarket.Server.Infrastructure.Data.Configurations.Base;
using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

namespace SunRaysMarket.Server.Infrastructure.Data.Configurations;

internal class TimeSlotDefinition : ConfigurationBase<PersistenceModels.TimeSlotDefinition>
{
    public override void Configure(EntityTypeBuilder<PersistenceModels.TimeSlotDefinition> builder)
    {
        base.Configure(builder);

        builder.ToTable("TimeSlotDefinitions");

        builder
            .HasIndex(
                timeSlotDefinition =>
                    new { timeSlotDefinition.StartTimeMinutes, timeSlotDefinition.EndTimeMinutes }
            )
            .IsUnique();

        builder.Property(timeSlotDefinition => timeSlotDefinition.StartTimeMinutes).IsRequired();

        builder.Property(timeSlotDefinition => timeSlotDefinition.EndTimeMinutes).IsRequired();

        builder.Property(timeSlotDefinition => timeSlotDefinition.OrderType).IsRequired();
    }
}
