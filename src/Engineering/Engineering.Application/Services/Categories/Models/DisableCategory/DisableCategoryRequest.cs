namespace Engineering.Application.Services.Categories.Models.DisableCategory;

public record DisableCategoryRequest(
    long Id)
    : IHttpRequest;