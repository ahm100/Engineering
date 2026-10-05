namespace Engineering.Application.Services.Categories.Models.GetCategoryById;

public record GetCategoryByIdRequest(
    long Id)
    : IHttpRequest;