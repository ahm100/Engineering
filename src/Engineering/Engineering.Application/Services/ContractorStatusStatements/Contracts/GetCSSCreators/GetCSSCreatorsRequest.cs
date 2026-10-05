namespace Engineering.Application.Services;

public record GetCSSCreatorsRequest(
    string? FilterData,
    int PageIndex,
    int PageSize) : IHttpRequest;
