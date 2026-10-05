using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithRolesInclude;

public record GetCostCenterWithRolesIncludeQuery(
    long Id
    ) : IQuery<CostCenter>;
