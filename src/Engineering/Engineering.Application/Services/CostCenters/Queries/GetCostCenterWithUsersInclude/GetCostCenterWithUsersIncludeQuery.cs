using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithUsersInclude;

public record GetCostCenterWithUsersIncludeQuery(
    long Id
    ) : IQuery<CostCenter>;
