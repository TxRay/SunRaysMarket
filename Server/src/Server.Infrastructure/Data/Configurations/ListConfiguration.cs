using SunRaysMarket.Server.Infrastructure.Data.Configurations.Base;
using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

namespace SunRaysMarket.Server.Infrastructure.Data.Configurations;

internal class ListConfiguration : TimeStampBaseConfiguration<List>
{
    public override void Configure(EntityTypeBuilder<List> builder)
    {
        base.Configure(builder);

        builder.HasIndex(list => list.Title).IsUnique();

        builder.HasIndex(list => list.Slug).IsUnique();

        builder.Property(list => list.Title).IsRequired().HasMaxLength(50);

        builder.Property(list => list.Slug).IsRequired().HasMaxLength(50);

        builder.Property(list => list.Description).IsRequired().HasColumnType("text");

        builder
            .HasOne(list => list.Department)
            .WithMany(department => department.Lists)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(list => list.Products)
            .WithMany(product => product.Lists)
            .UsingEntity<ListProduct>(lpBuilder =>
            {
                lpBuilder.HasIndex(lp => new { lp.ListId, lp.ProductId }).IsUnique();

                lpBuilder
                    .HasOne(listProduct => listProduct.Product)
                    .WithMany()
                    .HasForeignKey(listProduct => listProduct.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                lpBuilder
                    .HasOne(listProduct => listProduct.List)
                    .WithMany()
                    .HasForeignKey(listProduct => listProduct.ListId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
