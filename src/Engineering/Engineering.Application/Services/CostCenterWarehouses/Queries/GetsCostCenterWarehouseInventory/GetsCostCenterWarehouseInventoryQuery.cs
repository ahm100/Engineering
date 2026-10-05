
using CostCenterWarehouse = Engineering.Domain.Entities.CostCenters.CostCenterWarehouse;

namespace Engineering.Application.Services.CostCenterWarehouses.Queries.GetsCostCenterWarehouseInventory;

public record GetsCostCenterWarehouseInventoryQuery(
    long CostCenterId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<CostCenterWarehouse>>>;