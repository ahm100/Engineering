
namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleFiles;

public class DownloadMultipleFilesResponseModel
{
    [JsonProperty("data")]
    public List<FileDownloaded>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
