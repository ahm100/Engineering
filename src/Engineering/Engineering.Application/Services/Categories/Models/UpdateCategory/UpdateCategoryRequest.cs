namespace Engineering.Application.Services.Categories.Models.UpdateCategory;

public record UpdateCategoryRequest(
    long Id,
    string CategoryName,
    string CategoryCode,
    bool IsActive)
    : IHttpRequest;