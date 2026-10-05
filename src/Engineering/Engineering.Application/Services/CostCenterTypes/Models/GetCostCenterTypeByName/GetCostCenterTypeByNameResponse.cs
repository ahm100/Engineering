namespace Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByName;

public record GetCostCenterTypeByNameResponse
{
    public long Id { get; set; }
    public string CostCenterTypeCode { get; set; } = string.Empty;
    public string CostCenterTypeTitle { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}