using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterByName;

public record GetCostCenterByNameQuery(
    string CostCenterName,
    long? CompanyId
    ) : IQuery<CostCenter>;