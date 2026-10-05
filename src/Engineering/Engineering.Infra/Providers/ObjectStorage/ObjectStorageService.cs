using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadFile;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleFiles;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleFileStream;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleStaticFilesByNameStream;

namespace Engineering.Infra.Providers.ObjectStorage;

public class ObjectStorageService : IObjectStorageService
{
    private readonly IObjectStorageProvider _objectStorageProvider;

    public ObjectStorageService(IObjectStorageProvider objectStorageProvider)
    {
        _objectStorageProvider = objectStorageProvider;
    }

    public async Task<DownloadFileResponse> DownloadFile(DownloadFileRequest request, CT ct)
    {
        return await _objectStorageProvider.DownloadFile(request.Id, request.GetThumbnail, ct);
    }

    public async Task<DownloadMultipleStaticFilesByNameStreamResponse> DownloadMultipleStaticFilesByNameStream(DownloadMultipleStaticFilesByNameStreamRequest request, CT ct)
    {
        return await _objectStorageProvider.DownloadMultipleStaticFilesByNameStream(request.Names, request.Type, ct);
    }

    public async Task<DownloadMultipleFileStreamResponse> DownloadMultipleFileStream(DownloadMultipleFileStreamRequest request, CT ct)
    {
        return await _objectStorageProvider.DownloadMultipleFileStream(request.Ids, request.GetThumbnail, ct);
    }

    public async Task<DownloadMultipleFilesResponse> DownloadMultipleFiles(DownloadMultipleFilesRequest request, CT ct)
    {
        return await _objectStorageProvider.DownloadMultipleFiles(request.Ids, request.GetThumbnail, ct);
    }

}