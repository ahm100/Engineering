namespace Engineering.Application.Services.CostOvers.Models.CreateCostOver;

public record CreateCostOverRequest(
    string CostOverCode,
    string CostOverName,
    bool IsActive)
    : IHttpRequest;