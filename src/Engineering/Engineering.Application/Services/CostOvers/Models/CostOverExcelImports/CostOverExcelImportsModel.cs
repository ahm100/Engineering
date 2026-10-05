namespace Engineering.Application.Services.CostOvers.Models.CostOverExcelImports;

public record CostOverExcelImportsModel
{
    public string CostOverName { get; private set; } = string.Empty;
    public string CostOverCode { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;
}