using PlanerModel = Engineering.Application.WebServices.MetaDataServices.Planers.Models.Planer;

namespace Engineering.Application.WebServices.MetaDataServices.Planers.Models.GetPlanerById;

public class GetPlanerByIdResponse
{
    [JsonProperty("data")]
    public List<PlanerModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
