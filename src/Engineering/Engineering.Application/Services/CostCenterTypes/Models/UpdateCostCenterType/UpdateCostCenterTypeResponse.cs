namespace Engineering.Application.Services.CostCenterTypes.Models.UpdateCostCenterType;

public record UpdateCostCenterTypeResponse(
    long Id,
    string CostCenterTypeName,
    string CostCenterTypeCode);