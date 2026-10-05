
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetsActiveAuthorizedCostCenter;

public record GetsActiveAuthorizedCostCenterQuery(
    string? FilterData,
    long UserId,
    long? EmployerId,
    long? CostCenterTypeId,
    long? CityId,
    string? Code,
    string? Name,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<CostCenter>>>;