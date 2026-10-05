namespace Engineering.Application.Services.CostCenters.Models.GetCostCenterHistories;

public record GetCostCenterHistoriesRequest(
    long Id,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
