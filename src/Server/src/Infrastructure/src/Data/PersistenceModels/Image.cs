using SunRaysMarket.Server.Infrastructure.Data.PersistenceModels.Base;

namespace SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

internal class Image : TimeStampModelBase
{
    public Guid UrlIdentifier { get; set; }
    public string ContentType { get; set; } = null!;
    public string FileExtension { get; set; } = null!;
    public string UploadFileName { get; set; } = null!;
    public byte[] Data { get; set; } = null!;

    public string Url =>
        $"/api/images/{CreatedAt!.Value.Month}/{CreatedAt!.Value.Day}"
        + $"/{CreatedAt!.Value.Year}/{UrlIdentifier}{FileExtension}";
}