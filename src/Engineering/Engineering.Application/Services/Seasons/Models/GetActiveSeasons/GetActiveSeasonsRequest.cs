namespace Engineering.Application.Services.Seasons.Models.GetActiveSeasons;

public record GetActiveSeasonsRequest(
    string? FilterData,
    long? branchId,
    string? SeasonCode,
    string? SeasonName,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
