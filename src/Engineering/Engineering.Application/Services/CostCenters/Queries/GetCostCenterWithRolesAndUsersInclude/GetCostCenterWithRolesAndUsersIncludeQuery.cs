using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithRolesAndUsersInclude;

public record GetCostCenterWithRolesAndUsersIncludeQuery(
    long Id
    ) : IQuery<CostCenter>;
