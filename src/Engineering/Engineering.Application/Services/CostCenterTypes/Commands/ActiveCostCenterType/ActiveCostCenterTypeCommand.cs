using CostCenterType = Engineering.Domain.Entities.CostCenters.CostCenterType;

namespace Engineering.Application.Services.CostCenterTypes.Commands.ActiveCostCenterType;

public record ActiveCostCenterTypeCommand(
    CostCenterType Entity)
    : ICommand<CostCenterType>;