using Microsoft.AspNetCore.Http;

namespace SunRaysMarket.Server.Application.Repositories;

public interface IImageRepository
{
    Task<string> UploadAsync(IFormFile file);

    Task<string?> GetUrlAsync(Guid urlIdentifier);
    Task<ImageDownloadModel?> DownloadAsync(string urlHandle);
}
