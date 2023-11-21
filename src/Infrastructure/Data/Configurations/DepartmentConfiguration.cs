using Infrastructure.Data.Configurations.Base;
using Infrastructure.Data.PersistenceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal class DepartmentConfiguration : BaseConfiguration<Department>
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
