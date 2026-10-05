using CostCenterWarehouse = Engineering.Domain.Entities.CostCenters.CostCenterWarehouse;

namespace Engineering.Application.Services.CostCenterWarehouses.Commands.Delete;

public record DeleteCostCenterWarehouseCommand(
    long Id
    ) : ICommand<CostCenterWarehouse>;