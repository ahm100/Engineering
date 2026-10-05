using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithoutInclude;

public record GetCostCenterWithoutIncludeQuery(
    long Id
    ) : IQuery<CostCenter>;