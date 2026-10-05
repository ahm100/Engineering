using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Queries.GetCostOverByCode;

public record GetCostOverByCodeQuery(
    string CostOverCode,
    long? CompanyId)
    : IQuery<CostOver>;