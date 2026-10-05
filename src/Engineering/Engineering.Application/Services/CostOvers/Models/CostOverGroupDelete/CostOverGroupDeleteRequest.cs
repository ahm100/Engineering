namespace Engineering.Application.Services.CostOvers.Models.CostOverGroupDelete;

public record CostOverGroupDeleteRequest(
    List<long> Ids)
    : IHttpRequest;