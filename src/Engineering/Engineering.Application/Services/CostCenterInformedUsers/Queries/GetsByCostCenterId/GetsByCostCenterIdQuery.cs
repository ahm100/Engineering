
using CostCenterInformedUser = Engineering.Domain.Entities.CostCenters.CostCenterInformedUser;

namespace Engineering.Application.Services.CostCenterInformedUsers.Queries.GetsByCostCenterId;

public record GetsByCostCenterIdQuery(
    long CostCenterId
    ) : IQuery<DataResult<List<CostCenterInformedUser>>>;