namespace Engineering.Application.Services.CostOvers.Models.StateChangerCostOvers;

public record ActivateCostOversRequest(
    List<long> Ids)
    : IHttpRequest;