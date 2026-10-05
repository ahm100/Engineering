namespace Engineering.Application.Services.Categories.Models.GetCategoryByCode;

public record GetCategoryByCodeRequest(
    string CategoryCode)
    : IHttpRequest;