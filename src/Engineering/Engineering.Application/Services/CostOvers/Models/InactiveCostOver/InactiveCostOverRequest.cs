namespace Engineering.Application.Services.CostOvers.Models.InactiveCostOver;

public record InactiveCostOverRequest(
    long Id)
    : IHttpRequest;