namespace Engineering.Application.Services.Seasons.Models.SeasonModels;

public record GetSeasonsModel(
    long Id,
    string SeasonName,
    string SeasonCode,
    long BranchId,
    string BranchName,
    string BranchCode,
    bool IsActive,
    long? CompanyId,
    string? CompanyNameFa);
