namespace Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterTypeExcelExporter;

public record GetsCostCenterTypeExcelExporterModel
{
    public long Id { get; set; }
    public string CostCenterTypeTitle { get; set; } = string.Empty;
    public string CostCenterTypeCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}