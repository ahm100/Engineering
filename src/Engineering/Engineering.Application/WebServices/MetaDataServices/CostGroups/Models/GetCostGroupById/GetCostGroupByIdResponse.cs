using CostGroupModel = Engineering.Application.WebServices.MetaDataServices.CostGroups.Models.CostGroup;

namespace Engineering.Application.WebServices.MetaDataServices.MetaDataServices.Models.GetCostGroupById;

public class GetCostGroupByIdResponse
{
    [JsonProperty("value")]
    public CostGroupModel? Value { get; set; }
}
