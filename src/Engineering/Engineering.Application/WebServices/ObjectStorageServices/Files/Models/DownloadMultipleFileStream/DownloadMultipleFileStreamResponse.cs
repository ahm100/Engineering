
namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleFileStream;

public class DownloadMultipleFileStreamResponse
{
    [JsonProperty("value")]
    public DownloadMultipleFileStreamsModelValue Value { get; set; } = new();
}

public class DownloadMultipleFileStreamsModelValue
{
    [JsonProperty("files")]
    public List<DownloadMultipleFileStreamsModel> Files { get; set; } = new();
}

public record DownloadMultipleFileStreamsModel(
    byte[]? Content,
    string FileFormat,
    string FileName
    );