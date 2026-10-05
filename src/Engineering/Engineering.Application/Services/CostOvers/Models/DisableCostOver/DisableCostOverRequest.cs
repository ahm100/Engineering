namespace Engineering.Application.Services.CostOvers.Models.DisableCostOver;

public record DisableCostOverRequest(
    long Id)
    : IHttpRequest;