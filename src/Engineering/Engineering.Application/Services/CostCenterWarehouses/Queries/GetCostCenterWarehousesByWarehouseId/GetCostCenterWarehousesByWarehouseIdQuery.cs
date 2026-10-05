using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterWarehouses.Queries.GetCostCenterWarehousesByWarehouseId;

public record GetCostCenterWarehousesByWarehouseIdQuery(List<long> WarehouseIds) : IQuery<List<CostCenterWarehouse>>;