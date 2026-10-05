namespace Engineering.Application.Services.Seasons.Models.UpdateSeason;

public record UpdateSeasonResponse(
    long Id,
    string SeasonName,
    string SeasonCode,
    long BranchId,
    string BranchName,
    string BranchCode,
    bool IsActive,
    long? CompanyId,
    string? CompanyNameFa
    );
