namespace Engineering.Application.Services.CostCenterTypes.Models.GetsActiveCostCenterTypes;

public record GetsActiveCostCenterTypesRequest(
    string? FilterData,
    string? Code,
    string? Name,
    int PageIndex,
    int PageSize)
    : IHttpRequest;