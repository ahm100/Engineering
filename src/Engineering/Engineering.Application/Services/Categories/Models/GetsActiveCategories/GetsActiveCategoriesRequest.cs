namespace Engineering.Application.Services.Categories.Models.GetsActiveCategories;

public record GetsActiveCategoriesRequest(
    string? FilterData,
    string? Code,
    string? Name,
    int PageIndex,
    int PageSize)
    : IHttpRequest;