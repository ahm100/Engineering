using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterByCode;

public record GetCostCenterByCodeQuery(
    string CostCenterCode,
    long? CompanyId
    ) : IQuery<CostCenter>;