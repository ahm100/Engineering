
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetsByTypeId;

public record GetsByTypeIdQuery(
    long TypeId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<CostCenter>>>;