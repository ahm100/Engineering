
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetsCostCenterByIds;

public record GetsCostCenterByIdsQuery(
    List<long>? Ids,
    List<Guid>? PreferentialReferenceCodes,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<CostCenter>>>;