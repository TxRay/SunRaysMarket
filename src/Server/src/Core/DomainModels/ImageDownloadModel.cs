namespace SunRaysMarket.Server.Core.DomainModels;

public class ImageDownloadModel
{
    public string ContentType { get; set; } = null!;
    public byte[] Data { get; set; } = null!;
}
