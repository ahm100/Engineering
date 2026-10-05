using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Queries.GetCostOverById;

public record GetCostOverByIdQuery(
    long Id)
    : IQuery<CostOver?>;