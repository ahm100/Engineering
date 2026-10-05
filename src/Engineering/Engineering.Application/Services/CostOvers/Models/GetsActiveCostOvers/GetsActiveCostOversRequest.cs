namespace Engineering.Application.Services.CostOvers.Models.GetsActiveCostOvers;

public record GetsActiveCostOversRequest(
    string? FilterData,
    string? Code,
    string? Name,
    int PageIndex,
    int PageSize)
    : IHttpRequest;