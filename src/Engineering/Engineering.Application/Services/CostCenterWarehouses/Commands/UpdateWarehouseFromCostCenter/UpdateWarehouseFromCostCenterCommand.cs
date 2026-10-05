using CostCenterWarehouse = Engineering.Domain.Entities.CostCenters.CostCenterWarehouse;

namespace Engineering.Application.Services.CostCenterWarehouses.Commands.UpdateWarehouseFromCostCenter;

public record UpdateWarehouseFromCostCenterCommand(
    long Id,
    long WarehouseId,
    bool IsDefault
    ) : ICommand<CostCenterWarehouse>;