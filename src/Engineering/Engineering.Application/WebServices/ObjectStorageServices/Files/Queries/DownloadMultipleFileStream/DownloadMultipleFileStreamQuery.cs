using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleFileStream;

namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Queries.DownloadMultipleFileStream;

public record DownloadMultipleFileStreamQuery(
    List<Guid> Ids,
    bool GetThumbnail
    ) : IQuery<DownloadMultipleFileStreamsModelValue?>;
