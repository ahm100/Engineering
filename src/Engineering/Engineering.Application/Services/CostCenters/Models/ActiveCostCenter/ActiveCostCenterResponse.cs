
namespace Engineering.Application.Services.CostCenters.Models.ActiveCostCenter;

public record ActiveCostCenterResponse(
    long Id,
    string CostCenterCode,
    string CostCenterName,
    bool IsActive
    );
