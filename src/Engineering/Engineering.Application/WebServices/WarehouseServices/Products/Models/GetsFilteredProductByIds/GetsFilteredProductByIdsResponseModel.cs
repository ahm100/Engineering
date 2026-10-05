using ProductModel = Gita.Backend.Shared.Application.WebServices.WarehouseServices.Products.Models.Product;

namespace Engineering.Application.WebServices.WarehouseServices.Products.Models.GetsFilteredProductByIds;

public class GetsFilteredProductByIdsResponseModel
{
    [JsonProperty("data")]
    public List<ProductModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
