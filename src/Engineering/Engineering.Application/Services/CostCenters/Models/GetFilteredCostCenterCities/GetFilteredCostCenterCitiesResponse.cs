namespace Engineering.Application.Services.CostCenters.Models.GetFilteredCostCenterCities;

public record GetFilteredCostCenterCitiesResponse(
    List<GetFilteredCostCenterCitiesModel> Data,
    int RowCount);

public record GetFilteredCostCenterCitiesModel
{
    public long? CityId { get; set; }
    public string? CityName { get; set; }
    public string? CityCode { get; set; }
}