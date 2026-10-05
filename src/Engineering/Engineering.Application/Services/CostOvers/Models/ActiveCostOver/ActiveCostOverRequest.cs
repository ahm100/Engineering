namespace Engineering.Application.Services.CostOvers.Models.ActiveCostOver;

public record ActiveCostOverRequest(
    long Id)
    : IHttpRequest;