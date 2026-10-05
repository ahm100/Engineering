namespace Engineering.Application.Services.Categories.Models.CategoryGroupDelete;

public record CategoryGroupDeleteRequest(
    List<long> Ids)
    : IHttpRequest;