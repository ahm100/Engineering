using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterById;

public record GetCostCenterByIdQuery(
    long Id
    ) : IQuery<CostCenter>;