using SunRaysMarket.Server.Infrastructure.Data.Configurations.Base;
using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

namespace SunRaysMarket.Server.Infrastructure.Data.Configurations;

internal class OrderLineConfig : TimeStampConfigurationBase<PersistenceModels.OrderLine>
{
    public override void Configure(EntityTypeBuilder<PersistenceModels.OrderLine> builder)
    {
        base.Configure(builder);

        builder.HasIndex(orderLine => new { orderLine.OrderId, orderLine.ItemId }).IsUnique();

        builder.Property(orderLine => orderLine.ItemId).IsRequired();
        builder.Property(orderLine => orderLine.OrderId).IsRequired();
        builder.Property(orderLine => orderLine.Quantity).IsRequired();
        builder.Property(orderLine => orderLine.Price).IsRequired();
        builder.Property(orderLine => orderLine.Discount).IsRequired();
        builder.Property(orderLine => orderLine.TotalPrice).IsRequired();

        builder
            .HasOne(orderLine => orderLine.Order)
            .WithMany(order => order.OrderLines)
            .HasForeignKey(orderLine => orderLine.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(orderLine => orderLine.Order)
            .WithMany(order => order.OrderLines)
            .HasForeignKey(orderLine => orderLine.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(orderLine => orderLine.OrderSubstitution)
            .WithOne(orderSubstution => orderSubstution.OrderLine)
            .HasForeignKey<PersistenceModels.OrderLine>(orderLine => orderLine.OrderSubstitutionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}