namespace Engineering.Application.Services.Categories.Models.StateChangerCategories;

public record InactivateCategoriesRequest(
    List<long> Ids)
    : IHttpRequest;