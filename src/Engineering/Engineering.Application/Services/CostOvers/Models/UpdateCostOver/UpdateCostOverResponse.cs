namespace Engineering.Application.Services.CostOvers.Models.UpdateCostOver;

public record UpdateCostOverResponse(
    long Id,
    string CostOverName,
    string CostOverCode,
    bool IsActive);