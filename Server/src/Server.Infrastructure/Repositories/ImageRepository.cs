using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using SunRaysMarket.Server.Application.Repositories;
using SunRaysMarket.Server.Infrastructure.Cache;

namespace SunRaysMarket.Server.Infrastructure.Repositories;

internal class ImageRepository(ApplicationDbContext dbContext, IDistributedCache cache)
    : IImageRepository
{
    public async Task<string> UploadAsync(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var data = memoryStream.ToArray();

        var model = new Image
        {
            UrlIdentifier = Guid.NewGuid(),
            ContentType = file.ContentType,
            FileExtension = Path.GetExtension(file.FileName),
            UploadFileName = file.FileName,
            Data = data
        };

        await dbContext.Images.AddAsync(model);

        return model.UrlIdentifier.ToString();
    }

    public async Task<string?> GetUrlAsync(Guid urlIdentifier)
    {
        return await dbContext
            .Images
            .Where(i => i.UrlIdentifier == urlIdentifier)
            .Select(i => i.Url)
            .FirstOrDefaultAsync();
    }

    public async Task<ImageDownloadModel?> DownloadAsync(string urlHandle)
    {
        return await cache.SetOrFetchAsync(
            $"Image_{urlHandle}",
            async () =>
            {
                var handleSplit = urlHandle.Split(".");
                var urlIdentifier = Guid.Parse(handleSplit[0]);

                return await dbContext
                    .Images
                    .Where(i => i.UrlIdentifier == urlIdentifier)
                    .Select(
                        i => new ImageDownloadModel { ContentType = i.ContentType, Data = i.Data }
                    )
                    .FirstOrDefaultAsync();
            }
        );
    }
}
