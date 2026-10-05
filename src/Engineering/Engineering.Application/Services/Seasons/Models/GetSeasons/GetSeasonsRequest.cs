namespace Engineering.Application.Services.Seasons.Models.GetSeasons;

public record GetSeasonsRequest(
    string? FilterData,
    long? BranchId,
    string? SeasonCode,
    string? SeasonName,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
