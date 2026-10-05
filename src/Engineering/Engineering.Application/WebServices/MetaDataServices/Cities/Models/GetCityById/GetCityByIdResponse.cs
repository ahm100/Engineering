using CityModel = Engineering.Application.WebServices.MetaDataServices.Cities.Models.City;

namespace Engineering.Application.WebServices.MetaDataServices.Cities.Models.GetCityById;

public class GetCityByIdResponse
{
    [JsonProperty("value")]
    public CityModel? Value { get; set; }
}
