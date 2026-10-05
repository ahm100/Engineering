using System.ComponentModel;

namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Models;

public record FileDownloaded
{
    public IResult? Result { get; set; }
}

public enum SubSystemType
{
    [Description("engineering")]
    Engineering = 1,
    [Description("warehouse")]
    Warehouse = 2,
    [Description("commerce")]
    Commerce = 3,
    [Description("finantial")]
    Finantial = 4,
    [Description("metadata")]
    Metadata = 5,
    [Description("identity")]
    Identity = 6,
}
