using System.ComponentModel;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.RGSupplyImport;

public record RGSupplyImportResponse(
    bool IsDone,
    FileContentResult? DocumentFile
    );

public enum RGSupplyImportErrorResponseEnum
{
    [Description("ProductCode")]
    ProductCode = 1,

    [Description("Count")]
    Count = 2,

    [Description("InputDate")]
    InputDate = 3,

    [Description("Description")]
    Description = 4,

    [Description("Message")]
    Message = 5
}

public class RGSupplyImportErrorResponseModel
{
    public string ProductCode { get; set; } = string.Empty;
    public decimal Count { get; set; }
    public DateTime InputDate { get; set; }
    public string? Description { get; set; }
    public string? Message { get; set; }
}