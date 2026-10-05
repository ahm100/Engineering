namespace Engineering.Application.Services.CostCenterTypes.Models.CreateCostCenterType;

public record CreateCostCenterTypeResponse(
    long Id,
    string CostCenterTypeCode,
    string CostCenterTypeName);