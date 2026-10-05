
using Engineering.Domain.Entities.Seasons;

namespace Engineering.Application.Services.Seasons.Queries.GetsSeasonByBranchIds;

public record GetsSeasonByBranchIdsQuery(
    List<long> BranchIds,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Season>>>;