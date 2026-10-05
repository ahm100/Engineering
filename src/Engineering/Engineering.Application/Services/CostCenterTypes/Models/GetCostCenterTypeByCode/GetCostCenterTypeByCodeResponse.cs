namespace Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByCode;

public record GetCostCenterTypeByCodeResponse
{
    public long Id { get; set; }
    public string CostCenterTypeCode { get; set; } = string.Empty;
    public string CostCenterTypeTitle { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}