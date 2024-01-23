using SunRaysMarket.Server.Infrastructure.Data.Configurations.Base;
using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

namespace SunRaysMarket.Server.Infrastructure.Data.Configurations;

internal class OrderConfiguration : TimeStampBaseConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder
            .HasIndex(
                order =>
                    new
                    {
                        order.CustomerId,
                        order.StoreId,
                        order.TimeSlotId,
                        order.OrderType
                    }
            )
            .IsUnique();

        builder
            .Property(x => x.OrderNumber)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("nextval('\"OrderNumbers\"')");
        builder.Property(x => x.Subtotal).IsRequired().HasDefaultValue(0f);
        builder.Property(x => x.Discount).IsRequired().HasDefaultValue(0f);
        builder.Property(x => x.Tax).IsRequired().HasDefaultValue(0f);
        builder.Property(x => x.Total).IsRequired().HasDefaultValue(0f);
        builder.Property(order => order.Status).IsRequired().HasDefaultValue(OrderStatus.Received);

        builder
            .HasOne(order => order.DeliveryAddress)
            .WithMany()
            .HasForeignKey(order => order.DeliveryAddressId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(order => order.Customer)
            .WithMany(customer => customer.Orders)
            .HasForeignKey(order => order.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(order => order.Store)
            .WithMany(store => store.Orders)
            .HasForeignKey(order => order.StoreId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(order => order.TimeSlot)
            .WithMany(timeSlot => timeSlot.Orders)
            .HasForeignKey(order => order.TimeSlotId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
