
using CostCenterWarehouse = Engineering.Domain.Entities.CostCenters.CostCenterWarehouse;

namespace Engineering.Application.Services.CostCenterWarehouses.Queries.GetsByCostCenterId;

public record GetsCostCenterWarehouseByCostCenterIdQuery(
    long CostCenterId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<CostCenterWarehouse>>>;