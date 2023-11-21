using Infrastructure.Data.Configurations.Base;
using Infrastructure.Data.PersistenceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal class TimeSlotConfiguration : BaseConfiguration<TimeSlot>
{
    public override void Configure(EntityTypeBuilder<TimeSlot> builder)
    {
        base.Configure(builder);

        builder.ToTable("TimeSlots");

        builder
            .HasIndex(
                timeSlot =>
                    new
                    {
                        timeSlot.StoreId,
                        timeSlot.TimeSlotDefinitionId,
                        timeSlot.Date
                    }
            )
            .IsUnique();

        builder.Property(timeSlot => timeSlot.StoreId).IsRequired();

        builder.Property(timeSlot => timeSlot.TimeSlotDefinitionId).IsRequired();

        builder.Property(timeSlot => timeSlot.Date).IsRequired();

        builder.Property(timeSlot => timeSlot.Capacity).IsRequired();

        builder.Property(timeSlot => timeSlot.Filled).IsRequired().HasDefaultValue(0);

        builder
            .HasOne(timeSlot => timeSlot.Store)
            .WithMany(store => store.TimeSlots)
            .HasForeignKey(timeSlot => timeSlot.StoreId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
