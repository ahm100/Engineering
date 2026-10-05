using Engineering.Application.Services.CostOvers.Models.GetCostOverById;

namespace Engineering.Application.Services.CostOvers.Queries.GetCostOverByIdForResponse;

public record GetCostOverByIdForResponseQuery(
    long Id) : IQuery<GetCostOverByIdResponse?>;