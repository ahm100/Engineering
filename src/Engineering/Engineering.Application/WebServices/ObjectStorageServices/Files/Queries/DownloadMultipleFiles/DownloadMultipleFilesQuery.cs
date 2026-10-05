using Engineering.Application.WebServices.ObjectStorageServices.Files.Models;

namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Queries.DownloadMultipleFiles;

public record DownloadMultipleFilesQuery(
    List<Guid> Ids,
    bool IgnoreQuery
    ) : IQuery<DataResult<List<FileDownloaded>>>;
