
namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadFile;

public record DownloadFileRequest(
    Guid Id,
    bool GetThumbnail
    );
