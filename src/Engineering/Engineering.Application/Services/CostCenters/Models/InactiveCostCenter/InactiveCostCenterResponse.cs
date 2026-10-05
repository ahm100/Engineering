
namespace Engineering.Application.Services.CostCenters.Models.InactiveCostCenter;

public record InactiveCostCenterResponse(
    long Id,
    string CostCenterCode,
    string CostCenterName,
    bool IsActive
    );
