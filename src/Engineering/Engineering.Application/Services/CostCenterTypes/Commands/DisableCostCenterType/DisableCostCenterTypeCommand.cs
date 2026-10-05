using CostCenterType = Engineering.Domain.Entities.CostCenters.CostCenterType;

namespace Engineering.Application.Services.CostCenterTypes.Commands.DisableCostCenterType;

public record DisableCostCenterTypeCommand(
    CostCenterType Entity)
    : ICommand<CostCenterType>;