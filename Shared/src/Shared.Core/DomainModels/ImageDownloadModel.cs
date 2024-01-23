namespace SunRaysMarket.Shared.Core.DomainModels;

public class ImageDownloadModel
{
    public string ContentType { get; set; } = null!;
    public byte[] Data { get; set; } = null!;
}