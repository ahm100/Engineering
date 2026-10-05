namespace Engineering.Application.WebServices.MetaDataServices.Currencies.Models.GetsFilteredCities;

public record GetsFilteredCitiesResponse
{
    [JsonProperty("value")]
    public GetsFilteredCitiesModel? Value { get; set; }
}