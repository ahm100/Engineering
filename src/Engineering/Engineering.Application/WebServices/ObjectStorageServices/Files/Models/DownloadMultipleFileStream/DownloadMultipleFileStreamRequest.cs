
namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleFileStream;

public record DownloadMultipleFileStreamRequest(
    Guid[] Ids,
    bool GetThumbnail
    );
