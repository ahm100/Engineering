using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithInformedUsersInclude;

public record GetCostCenterWithInformedUsersIncludeQuery(
    long Id
    ) : IQuery<CostCenter>;