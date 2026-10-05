namespace Engineering.Application.Services.Branchs.Models.BranchModels;

public record SeasonModel(
    long Id,
    string SeasonName,
    string SeasonCode,
    bool IsActive);