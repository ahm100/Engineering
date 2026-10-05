using CategoryModel = Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models.WarehouseCategory;

namespace Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models.GetsWarehouseCategoryById;

public class GetsWarehouseCategoryByIdResponseModel
{
    [JsonProperty("data")]
    public List<CategoryModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
