namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProduct.GetsRequestGoodsSupplyProductNewExcelEnum;

public record GetRGSProductXlsxReportResponse(FileContentResult File);

public record GetsRequestGoodsSupplyProductNewExporterHeaderModel
{
    public string RGSRequestNumber { get; set; } = string.Empty;
    public string CostCenterName { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public DateTime? RequestedDate { get; set; }
    public string RequestedDateShamsi => RequestedDate?.ToShamsi();
    public string? Creator { get; set; } = string.Empty;
}

public record GetsRequestGoodsSupplyProductNewExporterModel
{
    public string RequestNumber { get; set; } = string.Empty;
    public string ProductCode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string MeasureName { get; set; } = string.Empty;
    public decimal? Count { get; set; }
    public DateTime? RequestedDate { get; set; }
    public string RequestedDateShamsi => RequestedDate?.ToShamsi();
    public string? Creator { get; set; } = string.Empty;

}