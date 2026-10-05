namespace Engineering.Application.Services.Seasons.Models.GetsSeasonByBranchIds;

public record GetsSeasonByBranchIdsRequest(
    List<long> BranchIds,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
