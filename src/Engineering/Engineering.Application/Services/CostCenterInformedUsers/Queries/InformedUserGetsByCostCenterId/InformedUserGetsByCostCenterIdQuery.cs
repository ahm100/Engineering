
using CostCenterInformedUser = Engineering.Domain.Entities.CostCenters.CostCenterInformedUser;

namespace Engineering.Application.Services.CostCenterInformedUsers.Queries.InformedUserGetsByCostCenterId;

public record InformedUserGetsByCostCenterIdQuery(
    long CostCenterId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<CostCenterInformedUser>>>;