
using CostCenterAuthorizedRole = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedRole;

namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Queries.GetsByAuthorizedRoleId;

public record GetsByCostCenterIdQuery(
    long CostCenterId
    ) : IQuery<DataResult<List<CostCenterAuthorizedRole>>>;