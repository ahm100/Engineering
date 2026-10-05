using Engineering.Application.Services.CostOvers.Models.GetCostOverByCode;

namespace Engineering.Application.Services.CostOvers.Queries.GetCostOverByCodeForResponse;

public record GetCostOverByCodeForResponseQuery(
    string CostOverCode)
    : IQuery<GetCostOverByCodeResponse?>;