namespace Engineering.Application.Services.CostOvers.Models.StateChangerCostOvers;

public record StateChangerCostOversRequest(
    List<long> Ids,
    bool State)
    : IHttpRequest;