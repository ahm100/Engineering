namespace Engineering.Application.Services.Categories.Models.UpdateCategory;

public record UpdateCategoryResponse(
    long Id,
    string CategoryName,
    string CategoryCode,
    bool IsActive);