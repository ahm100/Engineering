
namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleStaticFilesByNameStream;

public record DownloadMultipleStaticFilesByNameStreamRequest(
    string[] Names,
    SubSystemType Type
    );
