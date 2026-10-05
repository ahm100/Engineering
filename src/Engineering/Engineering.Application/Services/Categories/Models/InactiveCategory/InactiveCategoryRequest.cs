namespace Engineering.Application.Services.Categories.Models.InactiveCategory;

public record InactiveCategoryRequest(
    long Id)
    : IHttpRequest;