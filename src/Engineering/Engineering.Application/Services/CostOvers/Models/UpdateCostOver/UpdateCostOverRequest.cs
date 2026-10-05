namespace Engineering.Application.Services.CostOvers.Models.UpdateCostOver;

public record UpdateCostOverRequest(
    long Id,
    string CostOverName,
    string CostOverCode,
    bool IsActive)
    : IHttpRequest;