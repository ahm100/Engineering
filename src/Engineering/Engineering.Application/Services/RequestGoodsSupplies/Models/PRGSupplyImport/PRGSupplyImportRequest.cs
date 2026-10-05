namespace Engineering.Application.Services.RequestGoodsSupplies.Models.PRGSupplyImport;

public class PRGSupplyImportRequest : IHttpRequest
{
    public long ProjectId { get; set; }
    public bool IsDraft { get; set; } = false;
    public required IFormFile DocumentFile { get; set; }
}

public record PRGSupplyImportModel
{
    public string ProductCode { get; set; } = string.Empty;
    public int Count { get; set; }
    public DateTime InputDate { get; set; }
    public string Description { get; set; } = string.Empty;
}