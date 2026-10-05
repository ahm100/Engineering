
using CostCenterWarehouse = Engineering.Domain.Entities.CostCenters.CostCenterWarehouse;

namespace Engineering.Application.Services.CostCenterWarehouses.Queries.GetById;

public record GetCostCenterWarehouseByIdQuery(
    long Id
    ) : IQuery<CostCenterWarehouse?>;