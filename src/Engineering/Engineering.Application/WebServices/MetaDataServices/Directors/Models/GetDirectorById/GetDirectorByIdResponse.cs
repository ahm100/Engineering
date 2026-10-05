using DirectorModel = Engineering.Application.WebServices.MetaDataServices.Directors.Models.Director;

namespace Engineering.Application.WebServices.MetaDataServices.Directors.Models.GetDirectorById;

public class GetDirectorByIdResponse
{
    [JsonProperty("data")]
    public List<DirectorModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
