using Engineering.Application.Services.Seasons.Models.GetsSeasonByBranchIds;

namespace Engineering.Application.Services.Seasons.Queries.GetsSeasonByBranchIdsForResponse;

public record GetsSeasonByBranchIdsForResponseQuery(
    List<long> BranchIds,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsSeasonByBranchIdsModel>?>?>;