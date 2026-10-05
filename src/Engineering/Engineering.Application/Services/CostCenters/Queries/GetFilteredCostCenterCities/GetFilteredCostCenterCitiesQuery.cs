namespace Engineering.Application.Services.CostCenters.Queries.GetFilteredCostCenterCities;

public record GetFilteredCostCenterCitiesQuery(
    List<long>? CostCenterIds
    ) : IQuery<List<long>>;