namespace Engineering.Application.Services.Seasons.Models.CreateSeason;

public record CreateSeasonRequest(
    long BranchId,
    string SeasonCode,
    string SeasonName,
    bool IsActive
     ) : IHttpRequest;
