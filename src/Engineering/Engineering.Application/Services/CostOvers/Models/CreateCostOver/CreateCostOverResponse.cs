namespace Engineering.Application.Services.CostOvers.Models.CreateCostOver;

public record CreateCostOverResponse(
    long Id,
    string CostOverCode,
    string CostOverName,
    bool IsActive);