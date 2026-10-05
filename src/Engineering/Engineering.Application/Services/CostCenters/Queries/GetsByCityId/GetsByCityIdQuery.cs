
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetsByCityId;

public record GetsByCityIdQuery(
    long CityId,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<CostCenter>>>;