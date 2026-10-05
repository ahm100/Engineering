using CostCenterWarehouse = Engineering.Domain.Entities.CostCenters.CostCenterWarehouse;

namespace Engineering.Application.Services.CostCenterWarehouses.Commands.DeleteWarehouseFromCostCenter;

public record DeleteWarehouseFromCostCenterCommand(
    long Id
    ) : ICommand<CostCenterWarehouse>;
