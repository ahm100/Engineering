using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterTypes.Commands.StateChangerCostCenterTypes;

public record StateChangerCostCenterTypesCommand(
    List<CostCenterType> Items,
    bool State)
    : ICommand<bool?>;