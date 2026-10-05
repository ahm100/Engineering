namespace Engineering.Application.Services.CostCenterTypes.Models.StateChangerCostCenterTypes;

public record StateChangerCostCenterTypesRequest(
    List<long> Ids,
    bool State)
    : IHttpRequest;