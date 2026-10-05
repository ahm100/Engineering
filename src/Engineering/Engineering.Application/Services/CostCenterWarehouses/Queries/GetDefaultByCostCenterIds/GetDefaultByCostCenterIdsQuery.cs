using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterWarehouses.Queries.GetDefaultByCostCenterIds;

public record GetDefaultByCostCenterIdsQuery(List<long> CostCenterIds) : IQuery<List<CostCenterWarehouse>>;