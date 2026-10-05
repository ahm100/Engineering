using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadFile;

namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Queries.DownloadFile;

public class DownloadFileQueryHandler : IQueryHandler<DownloadFileQuery, FileDownloaded?>
{
    private readonly ILogger<DownloadFileQueryHandler> _logger;
    private readonly IObjectStorageService _objectStorageService;

    public DownloadFileQueryHandler(ILogger<DownloadFileQueryHandler> logger, IObjectStorageService objectStorageService)
    {
        _logger = logger;
        _objectStorageService = objectStorageService;
    }

    public async Task<Result<FileDownloaded?>> Handle(DownloadFileQuery request, CT ct)
    {
        try
        {
            var result = await _objectStorageService.DownloadFile(request.Adapt<DownloadFileRequest>(), ct);

            return result?.Value ?? Result.Failure<FileDownloaded?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FileDownloaded?>(SharedErrors.UnknownError);
        }
    }
}
