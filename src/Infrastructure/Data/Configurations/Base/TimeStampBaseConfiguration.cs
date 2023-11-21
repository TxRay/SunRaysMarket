using Infrastructure.Data.PersistenceModels.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Base;

internal abstract class TimeStampBaseConfiguration<T> : IEntityTypeConfiguration<T>
    where T : TimeStampBaseModel
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(entity => entity.Id);
        builder.Property(entity => entity.Id).ValueGeneratedOnAdd();
        builder
            .Property(entity => entity.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();
        builder
            .Property(entity => entity.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAddOrUpdate();
    }
}
