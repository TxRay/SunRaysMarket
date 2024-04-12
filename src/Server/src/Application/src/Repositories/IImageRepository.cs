using Microsoft.AspNetCore.Http;
using SunRaysMarket.Server.Core.DomainModels;

namespace SunRaysMarket.Server.Application.Repositories;

public interface IImageRepository
{
    Task<string> UploadAsync(IFormFile file);

    Task<string?> GetUrlAsync(Guid urlIdentifier);
    Task<ImageDownloadModel?> DownloadAsync(string urlHandle);
}