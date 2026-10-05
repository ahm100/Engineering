using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterByIdIncludeless;

public record GetCostCenterByIdIncludelessQuery(
    long Id
    ) : IQuery<CostCenter>;