
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetsByAuthorizedUserId;

public record GetsByAuthorizedUserIdQuery(
    string? FilterData,
    long UserId,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<CostCenter>>>;