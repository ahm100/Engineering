namespace Engineering.Application.WebServices.WarehouseServices.Warehouse.Models.GetWarehouseByIds;

public class GetWarehouseByIdsResponseModel
{
    [JsonProperty("data")]
    public List<GetWarehouseByIdsModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
