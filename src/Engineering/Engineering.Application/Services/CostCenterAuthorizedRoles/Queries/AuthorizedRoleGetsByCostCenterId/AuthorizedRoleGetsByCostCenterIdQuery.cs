
using CostCenterAuthorizedRole = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedRole;

namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Queries.AuthorizedRoleGetsByCostCenterId;

public record AuthorizedRoleGetsByCostCenterIdQuery(
    long CostCenterId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<CostCenterAuthorizedRole>>>;