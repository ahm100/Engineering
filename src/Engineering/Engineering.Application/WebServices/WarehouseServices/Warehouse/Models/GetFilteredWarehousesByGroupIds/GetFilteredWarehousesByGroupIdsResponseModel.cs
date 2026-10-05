namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredWarehousesByGroupIds;

public class GetFilteredWarehousesByGroupIdsResponseModel
{
    [JsonProperty("data")]
    public List<GetFilteredWarehousesByGroupIdsModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
