namespace Engineering.Application.Services.Categories.Models.GetsByFilterData;

public record GetsByFilterDataRequest(
    string? FilterData,
    string? CategoryFilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize)
    : IHttpRequest;