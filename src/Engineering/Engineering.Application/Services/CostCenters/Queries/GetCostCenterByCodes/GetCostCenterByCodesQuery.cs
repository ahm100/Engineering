using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterByCodes;

public record GetCostCenterByCodesQuery(
    List<string> CostCenterCodes,
    long? CompanyId
    ) : IQuery<List<CostCenter>?>;