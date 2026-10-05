using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByCode;

public record GetCostCenterTypeByCodeQuery(
    string CostCenterTypeCode,
    long? CompanyId)
    : IQuery<CostCenterType?>;