using CostCenterWarehouse = Engineering.Domain.Entities.CostCenters.CostCenterWarehouse;

namespace Engineering.Application.Services.CostCenterWarehouses.Commands.Update;

public record UpdateCostCenterWarehouseCommand(
    long Id,
    long WarehouseId,
    bool IsDefault
    ) : ICommand<CostCenterWarehouse>;