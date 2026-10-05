namespace Engineering.Application.Services.CostCenterTypes.Models.ActiveCostCenterType;

public record ActiveCostCenterTypeRequest(
    long Id)
    : IHttpRequest;