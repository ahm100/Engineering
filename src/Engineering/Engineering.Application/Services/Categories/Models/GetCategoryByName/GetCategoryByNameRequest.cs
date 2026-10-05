namespace Engineering.Application.Services.Categories.Models.GetCategoryByName;

public record GetCategoryByNameRequest(
    string CategoryName)
    : IHttpRequest;