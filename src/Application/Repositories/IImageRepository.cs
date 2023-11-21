using Microsoft.AspNetCore.Http;

namespace Application.Repositories;

public interface IImageRepository
{
    Task<string> UploadAsync(IFormFile file);

    Task<string?> GetUrlAsync(Guid urlIdentifier);
    Task<byte[]> DownloadAsync(string url);
}
