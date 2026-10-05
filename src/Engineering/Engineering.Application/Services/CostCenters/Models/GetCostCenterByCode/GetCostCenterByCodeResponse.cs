
namespace Engineering.Application.Services.CostCenters.Models.GetCostCenterByCode;

public record GetCostCenterByCodeResponse(
    long Id,
    string CostCenterTypeTitle,
    string CostCenterCode,
    string CostCenterName,
    bool IsActive,
    long? CompanyId,
    string? CompanyNameFa
    );
