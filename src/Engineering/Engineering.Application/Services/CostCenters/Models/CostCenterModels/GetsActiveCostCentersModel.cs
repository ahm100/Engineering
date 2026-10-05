namespace Engineering.Application.Services.CostCenters.Models.CostCenterModels;

public record GetsActiveCostCentersModel
{
    public long Id { get; set; }
    public string CostCenterCode { get; set; } = string.Empty;
    public string CostCenterName { get; set; } = string.Empty;
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public bool? IsDefault { get; set; }
}


