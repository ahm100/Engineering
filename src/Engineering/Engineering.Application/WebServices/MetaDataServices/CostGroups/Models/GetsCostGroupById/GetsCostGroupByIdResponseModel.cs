using CostGroupModel = Engineering.Application.WebServices.MetaDataServices.CostGroups.Models.CostGroup;

namespace Engineering.Application.WebServices.MetaDataServices.CostGroups.Models.GetsCostGroupById;

public class GetsCostGroupByIdResponseModel
{
    [JsonProperty("data")]
    public List<CostGroupModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
