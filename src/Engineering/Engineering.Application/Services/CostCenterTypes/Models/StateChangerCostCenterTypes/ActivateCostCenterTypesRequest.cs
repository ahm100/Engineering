namespace Engineering.Application.Services.CostCenterTypes.Models.StateChangerCostCenterTypes;

public record ActivateCostCenterTypesRequest(
    List<long> Ids)
    : IHttpRequest;