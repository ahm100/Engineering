
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetsCostCenterByProjectManagerId;

public record GetsCostCenterByProjectManagerIdQuery(
    string? FilterData,
    long ProjectManagerId,
    long? companyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<CostCenter>>>;
