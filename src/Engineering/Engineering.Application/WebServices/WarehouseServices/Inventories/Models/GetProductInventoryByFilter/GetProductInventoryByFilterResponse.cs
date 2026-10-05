
namespace Engineering.Application.WebServices.WarehouseServices.Inventories.Models.GetProductInventoryByFilter;

public class GetProductInventoryByFilterResponse
{
    [JsonProperty("value")]
    public InventoryModel? Value { get; set; }
}
