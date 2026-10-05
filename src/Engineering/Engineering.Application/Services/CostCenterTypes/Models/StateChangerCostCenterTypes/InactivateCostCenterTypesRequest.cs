namespace Engineering.Application.Services.CostCenterTypes.Models.StateChangerCostCenterTypes;

public record InactivateCostCenterTypesRequest(
    List<long> Ids)
    : IHttpRequest;