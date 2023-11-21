using Infrastructure.Data.Configurations.Base;
using Infrastructure.Data.PersistenceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal class TimeSlotDefinitionConfiguration : BaseConfiguration<TimeSlotDefinition>
{
    public override void Configure(EntityTypeBuilder<TimeSlotDefinition> builder)
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
