using Application.Repositories;
using Infrastructure.Data;
using Infrastructure.Data.PersistenceModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class ImageRepository : IImageRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ImageRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

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

        await _dbContext.Images.AddAsync(model);
        
        return model.UrlIdentifier.ToString();
    }

    public async Task<string?> GetUrlAsync(Guid urlIdentifier) =>
        await _dbContext.Images
            .Where(i => i.UrlIdentifier == urlIdentifier)
            .Select(i => i.Url)
            .FirstOrDefaultAsync();

    public Task<byte[]> DownloadAsync(string url)
    {
        throw new NotImplementedException();
    }
}
