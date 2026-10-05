namespace Engineering.Application.Services.Categories.Models.StateChangerCategories;

public record ActivateCategoriesRequest(
    List<long> Ids)
    : IHttpRequest;