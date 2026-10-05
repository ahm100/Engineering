using CostGroupModel = Engineering.Application.WebServices.MetaDataServices.CostGroups.Models.CostGroup;

namespace Engineering.Application.WebServices.MetaDataServices.CostGroups.Models.GetsCostGroupByCodes;

public class GetsCostGroupByCodesResponseModel
{
    [JsonProperty("data")]
    public List<CostGroupModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
