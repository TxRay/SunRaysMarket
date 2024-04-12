using SunRaysMarket.Server.Infrastructure.Data.Configurations.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.Configurations;

internal class DepartmentConfig : ConfigurationBase<Department>
{
    public override void Configure(EntityTypeBuilder<Department> builder)
    {
        base.Configure(builder);

        builder.HasIndex(department => department.Name).IsUnique();

        builder.HasIndex(department => department.Slug).IsUnique();

        builder.Property(department => department.Name).IsRequired().HasMaxLength(50);

        builder.Property(department => department.Slug).IsRequired().HasMaxLength(50);

        builder.Property(department => department.Description).IsRequired().HasColumnType("text");

        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

        builder.Property(x => x.Description).IsRequired().HasMaxLength(4000);
    }
}