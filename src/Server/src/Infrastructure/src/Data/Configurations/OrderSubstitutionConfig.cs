using SunRaysMarket.Server.Infrastructure.Data.Configurations.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.Configurations;

internal class OrderSubstitutionConfig : TimeStampConfigurationBase<OrderSubstitution>
{
    public override void Configure(EntityTypeBuilder<OrderSubstitution> builder)
    {
        base.Configure(builder);

        builder.HasIndex(orderSubstitution => orderSubstitution.OrderLineId).IsUnique();

        builder.Property(orderSubstitution => orderSubstitution.OrderLineId).IsRequired();
        builder.Property(orderSubstitution => orderSubstitution.OriginalItemId).IsRequired();
        builder.Property(orderSubstitution => orderSubstitution.SubstituteItemId).IsRequired();

        builder
            .HasOne(orderSubstitution => orderSubstitution.OriginalItem)
            .WithMany()
            .HasForeignKey(orderSubstitution => orderSubstitution.OriginalItemId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(orderSubstitution => orderSubstitution.SubstituteItem)
            .WithMany()
            .HasForeignKey(orderSubstitution => orderSubstitution.SubstituteItemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(orderSubstitution => orderSubstitution.OrderLine)
            .WithOne(orderLine => orderLine.OrderSubstitution)
            .HasForeignKey<OrderSubstitution>(orderSubstitution => orderSubstitution.OrderLineId);
    }
}