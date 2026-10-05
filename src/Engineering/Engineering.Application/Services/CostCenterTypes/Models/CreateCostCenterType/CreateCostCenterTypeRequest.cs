namespace Engineering.Application.Services.CostCenterTypes.Models.CreateCostCenterType;

public record CreateCostCenterTypeRequest(
    string CostCenterTypeCode,
    string CostCenterTypeName,
    bool IsActive)
    : IHttpRequest;