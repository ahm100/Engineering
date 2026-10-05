using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Queries.GetsCostOversByIds;

public record GetsCostOversByIdsQuery(
    List<long> Ids)
    : IQuery<DataResult<List<CostOver>>>;