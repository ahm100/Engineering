using Engineering.Application.Services.CostOvers.Models.GetCostOverByName;

namespace Engineering.Application.Services.CostOvers.Queries.GetCostOverByNameForResponse;

public record GetCostOverByNameForResponseQuery(
    string CostName) : IQuery<GetCostOverByNameResponse?>;