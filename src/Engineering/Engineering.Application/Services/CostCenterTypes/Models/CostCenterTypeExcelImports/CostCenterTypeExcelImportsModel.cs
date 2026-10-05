namespace Engineering.Application.Services.CostCenterTypes.Models.CostCenterTypeExcelImports;

public record CostCenterTypeExcelImportsModel
{
    public string CostCenterTypeName { get; private set; } = string.Empty;
    public string CostCenterTypeCode { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
}