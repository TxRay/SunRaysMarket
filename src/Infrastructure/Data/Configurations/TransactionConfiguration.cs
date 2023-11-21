using Infrastructure.Data.Configurations.Base;
using Infrastructure.Data.PersistenceModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal class TransactionConfiguration : TimeStampBaseConfiguration<Transaction>
{
    public override void Configure(EntityTypeBuilder<Transaction> builder)
    {
        base.Configure(builder);

        builder.Property(transaction => transaction.Code).IsRequired();

        builder.Property(transaction => transaction.CustomerId).IsRequired();

        builder.Property(transaction => transaction.OrderId).IsRequired();

        builder.Property(transaction => transaction.Status).IsRequired();

        builder.Property(transaction => transaction.PaymentMethod).IsRequired();

        builder.Property(transaction => transaction.AmountPaid).IsRequired();

        builder
            .HasOne(transaction => transaction.Customer)
            .WithMany(customer => customer.Transactions)
            .HasForeignKey(transaction => transaction.CustomerId);

        builder
            .HasOne(transaction => transaction.Order)
            .WithMany(order => order.Transactions)
            .HasForeignKey(transaction => transaction.OrderId);
    }
}
