namespace Engineering.Application.Services.Categories.Models.CategoryModels;

public record SeasonModel(
    long Id,
    string AlternativeId,
    string SeasonName,
    string SeasonCode,
    bool IsActive);