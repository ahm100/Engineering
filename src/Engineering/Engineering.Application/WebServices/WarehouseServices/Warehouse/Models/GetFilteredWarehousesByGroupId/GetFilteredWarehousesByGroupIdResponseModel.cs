namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredWarehousesByGroupId;

public class GetFilteredWarehousesByGroupIdResponseModel
{
    [JsonProperty("data")]
    public List<GetFilteredWarehousesByGroupIdModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
