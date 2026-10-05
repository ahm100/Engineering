using Engineering.Application.WebServices.ObjectStorageServices.Files.Models;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadFile;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleFiles;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleFileStream;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleStaticFilesByNameStream;
using Microsoft.AspNetCore.Mvc;

namespace Engineering.Infra.Providers.ObjectStorage;

public interface IObjectStorageProvider
{
    // دریافت فایل عکس تکی
    [Get("/v1/File/DownloadFile/")]
    Task<DownloadFileResponse> DownloadFile(
        [AliasAs("id")] Guid Id, [AliasAs("getThumbnail")] bool getThumbnail, CT ct);

    // دریافت فایل عکس تکی
    [Get("/v1/File/DownloadMultipleStaticFilesByNameStream/")]
    Task<DownloadMultipleStaticFilesByNameStreamResponse> DownloadMultipleStaticFilesByNameStream(
        [FromQuery][AliasAs("names")] string[] Names, [AliasAs("type")] SubSystemType type, CT ct);

    // دریافت فایل عکس تکی
    [Get("/v1/File/DownloadMultipleFileStream/")]
    Task<DownloadMultipleFileStreamResponse> DownloadMultipleFileStream(
        [Query(CollectionFormat.Multi)][AliasAs("ids")] Guid[] Ids, [AliasAs("getThumbnail")] bool GetThumbnail, CT ct);

    // دریافت فایل عکس گروهی
    [Get("/v1/File/DownloadMultipleFiles/")]
    Task<DownloadMultipleFilesResponse> DownloadMultipleFiles(
        [AliasAs("ids")] List<Guid> Ids, [AliasAs("getThumbnail")] bool getThumbnail, CT ct);

}