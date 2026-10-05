using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadFile;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleFiles;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleFileStream;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleStaticFilesByNameStream;

namespace Engineering.Application.Abstractions.Interfaces;

public interface IObjectStorageService
{
    Task<DownloadFileResponse> DownloadFile(
        DownloadFileRequest request,
        CT ct);

    Task<DownloadMultipleStaticFilesByNameStreamResponse> DownloadMultipleStaticFilesByNameStream(
        DownloadMultipleStaticFilesByNameStreamRequest request,
        CT ct);

    Task<DownloadMultipleFileStreamResponse> DownloadMultipleFileStream(
        DownloadMultipleFileStreamRequest request,
        CT ct);

    Task<DownloadMultipleFilesResponse> DownloadMultipleFiles(
        DownloadMultipleFilesRequest request,
        CT ct);

}