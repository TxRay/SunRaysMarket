using Infrastructure.Data.Configurations.Base;
using Infrastructure.Data.PersistenceModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal class ImageConfiguration : TimeStampBaseConfiguration<Image>
{
    public override void Configure(EntityTypeBuilder<Image> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.UrlIdentifier).IsUnique();

        builder.Property(x => x.UrlIdentifier).IsRequired();

        builder.Property(x => x.ContentType).IsRequired();

        builder.Property(x => x.FileExtension).IsRequired();

        builder.Property(x => x.UploadFileName).IsRequired();

        builder.Property(x => x.Data).IsRequired();
    }
}
