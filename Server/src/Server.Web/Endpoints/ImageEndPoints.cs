namespace SunRaysMarket.Server.Web.Endpoints;

public static class ImageEndPoints
{
    public static IEndpointRouteBuilder MapImageEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var imageGroup = endpoints
            .MapGroup("/Images")
            .WithGroupName("Images")
            .WithDescription("Endpoints for managing images.");

        imageGroup.MapImageDownloadEndpoint();

        return endpoints;
    }

    private static IEndpointRouteBuilder MapImageDownloadEndpoint(
        this IEndpointRouteBuilder endpoints
    )
    {
        endpoints.MapGet(
            "/{month:int}/{day:int}/{year:int}/{handle}",
            async (int month, int day, int year, string handle, IUnitOfWork unitOfWork) =>
            {
                var downloadModel = await unitOfWork.ImageRepository.DownloadAsync(handle);

                if (downloadModel is null)
                    return Results.NotFound("The image you are looking for does not exist.");

                var stream = new MemoryStream(downloadModel.Data);

                return Results.File(stream, downloadModel.ContentType);
            }
        );

        return endpoints;
    }
}
