using Engineering.Application.Services.CostCenters.Models.GetCostCenterHistories;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterHistory;

public record GetCostCenterHistoriesQuery(
    long Id,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IQuery<GetCostCenterHistoriesResponse?>;