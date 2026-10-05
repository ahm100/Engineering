namespace Engineering.Application.Services.Categories.Models.CreateCategory;

public record CreateCategoryResponse(
    long Id,
    string CategoryCode,
    string CategoryName,
    bool IsActive);