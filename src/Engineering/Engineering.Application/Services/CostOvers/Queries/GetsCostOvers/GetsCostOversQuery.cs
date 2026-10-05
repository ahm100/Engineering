using Engineering.Application.Services.CostOvers.Models.GetsCostOvers;

namespace Engineering.Application.Services.CostOvers.Queries.GetsCostOvers;

public record GetsCostOversQuery(
    List<long>? Ids,
    string? FilterData,
    string? Code,
    string? Name,
    bool? IsActive,
    string[]? OrderBy,
    long? CompanyId,
    int PageIndex,
    int PageSize)
    : IQuery<DataResult<List<GetsCostOversModel>>>;