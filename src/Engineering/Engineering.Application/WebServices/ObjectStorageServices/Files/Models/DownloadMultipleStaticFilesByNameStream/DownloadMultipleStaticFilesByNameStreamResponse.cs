
namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleStaticFilesByNameStream;

public class DownloadMultipleStaticFilesByNameStreamResponse
{
    [JsonProperty("value")]
    public DownloadMultipleStaticFilesByNameStreamsModelValue Value { get; set; } = new();
}

public class DownloadMultipleStaticFilesByNameStreamsModelValue
{
    [JsonProperty("files")]
    public List<DownloadMultipleStaticFilesByNameStreamsModel> Files { get; set; } = new();
}

public record DownloadMultipleStaticFilesByNameStreamsModel(byte[]? Content);