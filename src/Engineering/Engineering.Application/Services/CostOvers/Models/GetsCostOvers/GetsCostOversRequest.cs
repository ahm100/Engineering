namespace Engineering.Application.Services.CostOvers.Models.GetsCostOvers;

public record GetsCostOversRequest(
    string? FilterData,
    string? Code,
    string? Name,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize)
    : IHttpRequest;