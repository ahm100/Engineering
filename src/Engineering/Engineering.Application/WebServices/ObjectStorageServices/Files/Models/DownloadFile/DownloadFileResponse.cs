
namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadFile;

public class DownloadFileResponse
{
    [JsonProperty("value")]
    public FileDownloaded? Value { get; set; }
}
