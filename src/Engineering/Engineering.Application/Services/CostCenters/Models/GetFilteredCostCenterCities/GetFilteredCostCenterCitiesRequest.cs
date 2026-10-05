namespace Engineering.Application.Services.CostCenters.Models.GetFilteredCostCenterCities;

public record GetFilteredCostCenterCitiesRequest(
    List<long>? CostCenterIds,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
