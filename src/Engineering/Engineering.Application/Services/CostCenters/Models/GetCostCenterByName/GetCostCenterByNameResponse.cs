
namespace Engineering.Application.Services.CostCenters.Models.GetCostCenterByName;

public record GetCostCenterByNameResponse(
    long Id,
    string CostCenterTypeTitle,
    string CostCenterCode,
    string CostCenterName,
    bool IsActive,
    long? CompanyId,
    string? CompanyNameFa
    );
