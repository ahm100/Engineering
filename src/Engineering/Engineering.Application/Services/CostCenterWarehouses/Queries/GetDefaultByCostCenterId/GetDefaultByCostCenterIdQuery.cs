using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterWarehouses.Queries.GetDefaultByCostCenterId;

public record GetDefaultByCostCenterIdQuery(long costCenterId) : IQuery<CostCenterWarehouse>;
