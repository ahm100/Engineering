namespace Engineering.Application.Services.CostCenterTypes.Models.InactiveCostCenterType;

public record InactiveCostCenterTypeRequest(
    long Id)
    : IHttpRequest;