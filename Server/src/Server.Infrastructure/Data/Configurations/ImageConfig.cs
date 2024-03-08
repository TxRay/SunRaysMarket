using SunRaysMarket.Server.Infrastructure.Data.Configurations.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.Configurations;

internal class ImageConfig : TimeStampConfigurationBase<Image>
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
