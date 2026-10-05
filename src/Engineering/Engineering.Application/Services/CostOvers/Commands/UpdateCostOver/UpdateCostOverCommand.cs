using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Commands.UpdateCostOver;

public record UpdateCostOverCommand(
    long Id,
    string CostOverName,
    string CostOverCode,
    bool IsActive,
    long? CompanyId)
    : ICommand<CostOver>;