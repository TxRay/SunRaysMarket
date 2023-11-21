using Infrastructure.Data.Configurations.Base;
using Infrastructure.Data.PersistenceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal class OrderLineConfiguration : TimeStampBaseConfiguration<OrderLine>
{
    public override void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        base.Configure(builder);

        builder.HasIndex(orderLine => new { orderLine.OrderId, orderLine.ItemId }).IsUnique();

        builder.Property(orderLine => orderLine.ItemId).IsRequired();
        builder.Property(orderLine => orderLine.OrderId).IsRequired();
        builder.Property(orderLine => orderLine.UnitOfMeasurement).IsRequired();
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
            .HasForeignKey<OrderLine>(orderLine => orderLine.OrderSubstitutionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
