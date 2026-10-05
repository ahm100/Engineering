namespace Engineering.Application.Services.CostCenters.Models.GetsCostCenterByContractorId;

public record GetsCostCenterByContractorIdModel
{
    public long Id { get; set; }
    public string CostCenterCode { get; set; } = string.Empty;
    public string CostCenterName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
