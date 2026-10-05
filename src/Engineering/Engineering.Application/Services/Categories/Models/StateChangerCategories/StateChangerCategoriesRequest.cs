namespace Engineering.Application.Services.Categories.Models.StateChangerCategories;

public record StateChangerCategoriesRequest(
    List<long> Ids,
    bool State)
    : IHttpRequest;