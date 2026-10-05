namespace Engineering.Application.Services.Branchs.Models.GetsBranchByCategoryIds;

public record GetsBranchByCategoryIdsRequest(
    List<long> CategoryIds,
    string? FilterData,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize)
    : IHttpRequest;