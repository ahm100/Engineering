using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Commands.StateChangerCostCenters;

public record StateChangerCostCentersCommand(
    List<CostCenter> Items,
    bool State
    ) : ICommand<bool?>;
