using Infrastructure.Data.PersistenceModels.Base;

namespace Infrastructure.Data.PersistenceModels;

internal class Image : TimeStampBaseModel
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
