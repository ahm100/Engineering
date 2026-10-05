
namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleFiles;

public record DownloadMultipleFilesRequest(
    List<Guid> Ids,
    bool GetThumbnail
    );
