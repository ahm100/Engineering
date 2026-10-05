using Engineering.Application.WebServices.ObjectStorageServices.Files.Models;

namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Queries.DownloadFile;

public record DownloadFileQuery(
    Guid Id,
    bool GetThumbnail
    ) : IQuery<FileDownloaded?>;
