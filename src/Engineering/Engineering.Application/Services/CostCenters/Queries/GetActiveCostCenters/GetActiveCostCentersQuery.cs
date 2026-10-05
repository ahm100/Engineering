
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetActiveCostCenters;

public record GetActiveCostCentersQuery(
    string? FilterData,
    string? Name,
    string? Code,
    long? companyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<CostCenter>>>;