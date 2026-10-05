namespace Engineering.Application.Services.CostOvers.Models.StateChangerCostOvers;

public record InactivateCostOversRequest(
    List<long> Ids)
    : IHttpRequest;