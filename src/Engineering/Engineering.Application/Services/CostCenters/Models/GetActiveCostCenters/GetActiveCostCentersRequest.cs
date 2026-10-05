namespace Engineering.Application.Services.CostCenters.Models.GetActiveCostCenters;

public record GetActiveCostCentersRequest(
    string? FilterData,
    string? Name,
    string? Code,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
