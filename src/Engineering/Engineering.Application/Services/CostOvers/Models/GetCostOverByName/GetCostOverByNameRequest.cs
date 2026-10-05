namespace Engineering.Application.Services.CostOvers.Models.GetCostOverByName;

public record GetCostOverByNameRequest(
    string CostOverName)
    : IHttpRequest;