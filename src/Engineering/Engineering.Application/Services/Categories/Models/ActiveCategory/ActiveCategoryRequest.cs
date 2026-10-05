namespace Engineering.Application.Services.Categories.Models.ActiveCategory;

public record ActiveCategoryRequest(
    long Id)
    : IHttpRequest;