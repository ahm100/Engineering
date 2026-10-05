using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Commands.CreateCostOver;

public record CreateCostOverCommand(
    string CostOverName,
    string CostOverCode,
    bool IsActive,
    long? CompanyId)
    : ICommand<CostOver?>;