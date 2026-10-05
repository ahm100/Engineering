namespace Engineering.Application.Services.CostCenters.Models.CostCenterModels;

public record GetContractorCostCentersModel
{
    public long Id { get; set; }
    public string CostCenterCode { get; set; } = string.Empty;
    public string CostCenterName { get; set; } = string.Empty;
}


