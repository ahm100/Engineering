using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Queries.GetCostOverByName;

public record GetCostOverByNameQuery(
    string CostOverName,
    long? CompanyId)
    : IQuery<CostOver?>;