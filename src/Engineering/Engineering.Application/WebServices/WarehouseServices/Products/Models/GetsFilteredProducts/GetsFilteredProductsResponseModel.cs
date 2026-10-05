namespace Engineering.Application.WebServices.WarehouseServices.Products.Models.GetsFilteredProducts;

public class GetsFilteredProductsResponseModel
{
    [JsonProperty("Ids")]
    public List<long>? Ids { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
