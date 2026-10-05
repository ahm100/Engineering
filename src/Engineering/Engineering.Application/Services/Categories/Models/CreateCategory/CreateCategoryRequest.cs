namespace Engineering.Application.Services.Categories.Models.CreateCategory;

public record CreateCategoryRequest(
    string CategoryCode,
    string CategoryName,
    bool IsActive)
    : IHttpRequest;