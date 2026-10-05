using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenter;

public record GetCostCenterQuery(
    long Id
    ) : IQuery<CostCenter>;