using Engineering.Application.Services.CostOvers.Models.GetsActiveCostOvers;

namespace Engineering.Application.Services.CostOvers.Queries.GetsActiveCostOvers;

public record GetsActiveCostOversQuery(
    string? FilterData,
    string? Code,
    string? Name,
    int PageIndex,
    int PageSize)
    : IQuery<DataResult<List<GetsActiveCostOversModel>>>;