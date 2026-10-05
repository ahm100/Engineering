namespace Engineering.Application.Services.OperationInfos.Models.GetActiveOperationInfoBySeasonIds;

public record GetActiveOperationInfoBySeasonIdsRequest(
    List<long>? CategoryIds,
    List<long>? BranchIds,
    List<long>? SeasonIds,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;