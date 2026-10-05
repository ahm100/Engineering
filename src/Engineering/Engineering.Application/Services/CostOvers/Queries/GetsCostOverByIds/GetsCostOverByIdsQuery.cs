using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Queries.GetsCostOverByIds;

public record GetsCostOverByIdsQuery(
    List<long> Items)
    : IQuery<List<CostOver>>;