using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleFileStream;

namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Queries.DownloadMultipleFileStream;

public class DownloadMultipleFileStreamQueryHandler : IQueryHandler<DownloadMultipleFileStreamQuery, DownloadMultipleFileStreamsModelValue?>
{
    private readonly ILogger<DownloadMultipleFileStreamQueryHandler> _logger;
    private readonly IObjectStorageService _objectStorageService;

    public DownloadMultipleFileStreamQueryHandler(ILogger<DownloadMultipleFileStreamQueryHandler> logger, IObjectStorageService objectStorageService)
    {
        _logger = logger;
        _objectStorageService = objectStorageService;
    }

    public async Task<Result<DownloadMultipleFileStreamsModelValue?>> Handle(DownloadMultipleFileStreamQuery request, CT ct)
    {
        try
        {
            var result = await _objectStorageService.DownloadMultipleFileStream(request.Adapt<DownloadMultipleFileStreamRequest>(), ct);

            return result?.Value ?? Result.Failure<DownloadMultipleFileStreamsModelValue?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DownloadMultipleFileStreamsModelValue?>(SharedErrors.UnknownError);
        }
    }
}
