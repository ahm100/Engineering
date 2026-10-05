
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetsByAuthorizedRoleId;

public record GetsByAuthorizedRoleIdQuery(
    string? FilterData,
    long RoleId,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<CostCenter>>>;