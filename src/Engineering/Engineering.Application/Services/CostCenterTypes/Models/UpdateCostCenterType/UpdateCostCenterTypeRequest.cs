namespace Engineering.Application.Services.CostCenterTypes.Models.UpdateCostCenterType;

public record UpdateCostCenterTypeRequest(
    long Id,
    string CostCenterTypeName,
    string CostCenterTypeCode,
    bool IsActive)
    : IHttpRequest;