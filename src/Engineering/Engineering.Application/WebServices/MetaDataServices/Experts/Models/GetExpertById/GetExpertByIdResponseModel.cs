using ExpertModel = Engineering.Application.WebServices.MetaDataServices.Experts.Models.Expert;

namespace Engineering.Application.WebServices.MetaDataServices.Experts.Models.GetExpertById;

public class GetExpertByIdResponseModel
{
    [JsonProperty("data")]
    public List<ExpertModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
