
using CostCenterAuthorizedUser = Engineering.Domain.Entities.CostCenters.CostCenterAuthorizedUser;

namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Queries.GetsByAuthorizedUser;

public record GetsByCostCenterIdQuery(
    long CostCenterId
    ) : IQuery<DataResult<List<CostCenterAuthorizedUser>>>;