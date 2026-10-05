using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithWarehousesInclude;

public record GetCostCenterWithWarehousesIncludeQuery(
    long Id
    ) : IQuery<CostCenter>;