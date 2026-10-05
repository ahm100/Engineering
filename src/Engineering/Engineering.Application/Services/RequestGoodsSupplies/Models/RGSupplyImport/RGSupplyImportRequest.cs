namespace Engineering.Application.Services.RequestGoodsSupplies.Models.RGSupplyImport;

public class RGSupplyImportRequest : IHttpRequest
{
    public long ProjectOperationId { get; set; }
    public long? ProjectOperationDetailId { get; set; }
    public long OperationInfoSeasonId { get; set; }
    public required IFormFile DocumentFile { get; set; }
}

public record RGSupplyImportModel
{
    public string ProductCode { get; set; } = string.Empty;
    public int Count { get; set; }
    public DateTime InputDate { get; set; }
    public string Description { get; set; } = string.Empty;
}