namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSIdReport.Xslx;

public record GetDetailByRGSIdXslxReportResponse(FileContentResult File);

public record GetRGSNewExporterHeaderModel
{
    public string? ProjectName { get; set; } = string.Empty;
    public string? PurchaseLocation { get; set; } = string.Empty;
    public string? PurchaseReason { get; set; } = string.Empty;
    public string? RequestingOrganization { get; set; }
}

public record GetRequestGoodsSupplyDetailNewExporterModel
{
    public int Index { get; set; }
    public string? Reference { get; set; } = string.Empty;
    public string? ReferenceCode { get; set; } = string.Empty;
    public string? ProjectName { get; set; } = string.Empty;
    public string? Count { get; set; } = string.Empty;
    public string? CostCenterName { get; set; }
    public string? Creator { get; set; }
}