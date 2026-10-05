using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterWarehouses.Commands.Create;

public record CreateCostCenterWarehouseCommand(
    CostCenter CostCenter,
    long WarehouseId,
    bool IsDefault
    ) : ICommand<CostCenterWarehouse>;