namespace Engineering.Application.Services.CostOvers.Models.GetsCostOverExcelExporter;

public record GetsCostOverExcelExporterModel
{
    public long Id { get; set; }
    public string CostOverName { get; set; } = string.Empty;
    public string CostOverCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}