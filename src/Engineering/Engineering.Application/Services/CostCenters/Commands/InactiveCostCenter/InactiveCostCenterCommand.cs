using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Commands.InactiveCostCenter;

public record InactiveCostCenterCommand(
    long Id
    ) : ICommand<CostCenter>;