using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleStaticFilesByNameStream;

namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Queries.DownloadMultipleStaticFilesByNameStream;

public class DownloadMultipleStaticFilesByNameStreamQueryHandler : IQueryHandler<DownloadMultipleStaticFilesByNameStreamQuery, DownloadMultipleStaticFilesByNameStreamsModelValue?>
{
    private readonly ILogger<DownloadMultipleStaticFilesByNameStreamQueryHandler> _logger;
    private readonly IObjectStorageService _objectStorageService;

    public DownloadMultipleStaticFilesByNameStreamQueryHandler(ILogger<DownloadMultipleStaticFilesByNameStreamQueryHandler> logger, IObjectStorageService objectStorageService)
    {
        _logger = logger;
        _objectStorageService = objectStorageService;
    }

    public async Task<Result<DownloadMultipleStaticFilesByNameStreamsModelValue?>> Handle(DownloadMultipleStaticFilesByNameStreamQuery request, CT ct)
    {
        try
        {
            var result = await _objectStorageService.DownloadMultipleStaticFilesByNameStream(request.Adapt<DownloadMultipleStaticFilesByNameStreamRequest>(), ct);


            return result?.Value ?? Result.Failure<DownloadMultipleStaticFilesByNameStreamsModelValue?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DownloadMultipleStaticFilesByNameStreamsModelValue?>(SharedErrors.UnknownError);
        }
    }
}
