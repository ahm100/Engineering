namespace Engineering.Application.Services.CostOvers.Models.GetCostOverByCode;

public record GetCostOverByCodeRequest(
    string CostOverCode)
    : IHttpRequest;