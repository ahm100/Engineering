using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Commands.Delete;

public record DeleteCostCenterCommand(
    long Id
    ) : ICommand<CostCenter>;