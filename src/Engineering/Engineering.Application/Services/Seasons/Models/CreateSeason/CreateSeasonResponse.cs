namespace Engineering.Application.Services.Seasons.Models.CreateSeason;

public record CreateSeasonResponse(
    long Id,
    string SeasonCode,
    string SeasonName,
    long BranchId,
    string BranchName,
    bool IsActive);
