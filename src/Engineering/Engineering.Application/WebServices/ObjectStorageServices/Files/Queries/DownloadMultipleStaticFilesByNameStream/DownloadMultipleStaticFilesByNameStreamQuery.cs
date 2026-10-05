using Engineering.Application.WebServices.ObjectStorageServices.Files.Models;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleStaticFilesByNameStream;

namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Queries.DownloadMultipleStaticFilesByNameStream;

public record DownloadMultipleStaticFilesByNameStreamQuery(
    List<string> Names,
    SubSystemType Type
    ) : IQuery<DownloadMultipleStaticFilesByNameStreamsModelValue?>;
