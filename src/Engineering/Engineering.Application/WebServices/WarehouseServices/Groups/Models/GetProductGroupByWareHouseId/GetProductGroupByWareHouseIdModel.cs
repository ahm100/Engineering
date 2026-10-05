namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetProductGroupByWareHouseId;

public class GetProductGroupByWareHouseIdModel
{
    [JsonProperty("data")]
    public List<GroupsModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
