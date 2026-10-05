namespace Engineering.Application.Services.Categories.Models.CategoryModels;

public record BranchModel(
    long Id,
    string AlternativeId,
    string BranchName,
    string BranchCode,
    bool IsActive,
    List<SeasonModel>? Seasons);