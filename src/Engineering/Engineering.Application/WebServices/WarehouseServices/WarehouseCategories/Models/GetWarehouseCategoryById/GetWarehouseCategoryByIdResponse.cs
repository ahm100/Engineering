using CategoryModel = Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models.WarehouseCategory;

namespace Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models.GetWarehouseCategoryById;

public class GetWarehouseCategoryByIdResponse
{
    [JsonProperty("value")]
    public CategoryModel? Value { get; set; }
}
