namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSIdReport.Pdf;

public record GetDetailByRGSIdPdfReportHeaderModel
{
    public string ProjectName { get; set; } = string.Empty;
    public string PurchaseLocation { get; set; } = string.Empty;
    public string PurchaseReason { get; set; } = string.Empty;
    public string? RequestingOrganization { get; set; }
}

public record GetDetailByRGSIdPdfReportDetailNewExporterModel
{
    public string Index { get; set; }
    public string? Reference { get; set; } = string.Empty;
    public string? ReferenceCode { get; set; } = string.Empty;
    public string? ProjectName { get; set; } = string.Empty;
    public string? RequestedCount { get; set; } = string.Empty;
    public string? CostCenterName { get; set; }
    public string? Measure { get; set; }
    public string? Inventory { get; set; }
    public string? Creator { get; set; }
}

public record GetDetailByRGSIdPdfEnReportHeaderModel
{
    public string ProjectEnName { get; set; } = string.Empty;
    public string PurchaseLocation { get; set; } = string.Empty;
    public string PurchaseReason { get; set; } = string.Empty;
    public string? RequestingOrganizationEn { get; set; }
}

public record GetDetailByRGSIdPdfEnReportDetailNewExporterModel
{
    public string Index { get; set; }
    public string? ReferenceEn { get; set; } = string.Empty;
    public string? ReferenceCode { get; set; } = string.Empty;
    public string? ProjectEnName { get; set; } = string.Empty;
    public string? RequestedCount { get; set; } = string.Empty;
    public string? CostCenterEnName { get; set; }
    public string? Measure { get; set; }
    public string? Inventory { get; set; }
    public string? Creator { get; set; }
}