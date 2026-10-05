namespace Engineering.Application.Services.CostCenterTypes.Models.DisableCostCenterType;

public record DisableCostCenterTypeRequest(
    long Id)
    : IHttpRequest;