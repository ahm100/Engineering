namespace Engineering.Application.Services.Seasons.Models.UpdateSeason;

public record UpdateSeasonRequest(
    long Id,
    long BranchId,
    string SeasonName,
    string SeasonCode,
    bool IsActive
     ) : IHttpRequest;
