using MeasureunitModel = Engineering.Application.WebServices.MetaDataServices.Measureunits.Models.Measureunit;

namespace Engineering.Application.WebServices.MetaDataServices.Measureunits.Models.GetsMeasureunitById;

public class GetsMeasureunitByIdResponseModel
{
    [JsonProperty("data")]
    public List<MeasureunitModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
