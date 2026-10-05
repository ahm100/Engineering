using CostCenterType = Engineering.Domain.Entities.CostCenters.CostCenterType;

namespace Engineering.Application.Services.CostCenterTypes.Commands.InactiveCostCenterType;

public record InactiveCostCenterTypeCommand(
    CostCenterType Entity)
    : ICommand<CostCenterType>;