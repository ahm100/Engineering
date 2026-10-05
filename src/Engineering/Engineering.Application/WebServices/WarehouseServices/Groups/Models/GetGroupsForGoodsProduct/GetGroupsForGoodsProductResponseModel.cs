namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetGroupsForGoodsProduct;

public class GetGroupsForGoodsProductResponseModel
{
    [JsonProperty("data")]
    public List<GetGroupsForGoodsProductModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
