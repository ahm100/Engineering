namespace Engineering.Application.WebServices.WarehouseServices.Inventories.Models.GetProductInventoryByFilter;

public record InventoryModel
{
    [JsonProperty("data")]
    public List<FilteredInventory>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}

public record FilteredInventory(
    long? Id,
    string? GroupName,
    long? MeasureUnitId,
    string? MeasureUnitName,
    string? Name,
    string? Code,
    double? RealQuantity,
    long? WareHouseId,
    string? WareHouseCode,
    string? WareHouseName,
    long? BrandId,
    long? BrandModelId,
    string? Brand,
    string? BrandModel,
    double? RequestQuantity,
    double? PhysicalQuantity
    );

