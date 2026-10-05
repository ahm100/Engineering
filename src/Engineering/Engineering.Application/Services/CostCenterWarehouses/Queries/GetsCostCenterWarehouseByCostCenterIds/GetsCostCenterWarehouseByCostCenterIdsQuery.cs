
using CostCenterWarehouse = Engineering.Domain.Entities.CostCenters.CostCenterWarehouse;

namespace Engineering.Application.Services.CostCenterWarehouses.Queries.GetsCostCenterWarehouseByCostCenterIds;

public record GetsCostCenterWarehouseByCostCenterIdsQuery(
    List<long> CostCenterIds,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<CostCenterWarehouse>>>;