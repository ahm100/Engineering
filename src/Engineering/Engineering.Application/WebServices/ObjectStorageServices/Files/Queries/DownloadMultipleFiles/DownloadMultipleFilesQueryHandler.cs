using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleFiles;

namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Queries.DownloadMultipleFiles;

public class DownloadMultipleFilesQueryHandler : IQueryHandler<DownloadMultipleFilesQuery, DataResult<List<FileDownloaded>>>
{
    private readonly IObjectStorageService _services;
    private readonly ILogger<DownloadMultipleFilesQueryHandler> _logger;

    public DownloadMultipleFilesQueryHandler(ILogger<DownloadMultipleFilesQueryHandler> logger, IObjectStorageService services)
    {
        _logger = logger;
        _services = services;
    }

    public async Task<Result<DataResult<List<FileDownloaded>>?>> Handle(DownloadMultipleFilesQuery request, CT ct)
    {
        try
        {
            var result = await _services.DownloadMultipleFiles(request.Adapt<DownloadMultipleFilesRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<FileDownloaded>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<FileDownloaded>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<FileDownloaded>>>(SharedErrors.UnknownError);
        }
    }
}
