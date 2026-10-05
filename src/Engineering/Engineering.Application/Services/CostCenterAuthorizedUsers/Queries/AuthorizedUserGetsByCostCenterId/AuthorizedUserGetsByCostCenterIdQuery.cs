
using CostCenterAuthorizedUser = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedUser;

namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Queries.AuthorizedUserGetsByCostCenterId;

public record AuthorizedUserGetsByCostCenterIdQuery(
    long CostCenterId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<CostCenterAuthorizedUser>>>;