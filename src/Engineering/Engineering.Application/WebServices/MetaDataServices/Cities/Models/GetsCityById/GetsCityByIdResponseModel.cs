using CityModel = Engineering.Application.WebServices.MetaDataServices.Cities.Models.City;

namespace Engineering.Application.WebServices.MetaDataServices.Cities.Models.GetsCityById;

public class GetsCityByIdResponseModel
{
    [JsonProperty("data")]
    public List<CityModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
