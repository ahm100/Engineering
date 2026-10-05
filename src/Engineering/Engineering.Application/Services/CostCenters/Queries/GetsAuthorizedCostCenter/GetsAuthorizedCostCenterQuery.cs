using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenters.Queries.GetsAuthorizedCostCenter;

public record GetsAuthorizedCostCenterQuery(
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