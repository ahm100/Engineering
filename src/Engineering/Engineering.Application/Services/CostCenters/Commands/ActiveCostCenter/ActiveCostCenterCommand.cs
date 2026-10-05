using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Commands.ActiveCostCenter;

public record ActiveCostCenterCommand(
    long Id
    ) : ICommand<CostCenter>;