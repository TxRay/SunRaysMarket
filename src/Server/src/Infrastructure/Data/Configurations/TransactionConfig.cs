using SunRaysMarket.Server.Infrastructure.Data.Configurations.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.Configurations;

internal class TransactionConfig : TimeStampConfigurationBase<Transaction>
{
    public override void Configure(EntityTypeBuilder<Transaction> builder)
    {
        base.Configure(builder);

        builder
            .Property(transaction => transaction.TransactionNumber)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("nextval('\"TransactionNumbers\"')");

        builder.Property(transaction => transaction.OrderId).IsRequired();

        builder.Property(transaction => transaction.Status).IsRequired();

        builder.Property(transaction => transaction.PaymentMethod).IsRequired();

        builder.Property(transaction => transaction.AmountPaid).IsRequired();

        builder
            .HasOne(transaction => transaction.Order)
            .WithMany(order => order.Transactions)
            .HasForeignKey(transaction => transaction.OrderId);
    }
}
