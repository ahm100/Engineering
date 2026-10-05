using MeasureunitModel = Engineering.Application.WebServices.MetaDataServices.Measureunits.Models.Measureunit;

namespace Engineering.Application.WebServices.MetaDataServices.Measureunits.Models.GetMeasureunitById;

public class GetMeasureunitByIdResponse
{
    [JsonProperty("value")]
    public MeasureunitModel? Value { get; set; }
}
