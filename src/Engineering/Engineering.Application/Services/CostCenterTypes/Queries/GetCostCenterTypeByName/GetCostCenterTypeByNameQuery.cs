using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByName;

public record GetCostCenterTypeByNameQuery(
    string CostCenterTypeName,
    long? CompanyId)
    : IQuery<CostCenterType?>;