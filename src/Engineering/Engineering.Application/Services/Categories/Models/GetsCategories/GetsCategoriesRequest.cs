namespace Engineering.Application.Services.Categories.Models.GetsCategories;

public record GetsCategoriesRequest(
    string? FilterData,
    string? Code,
    string? Name,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize)
    : IHttpRequest;