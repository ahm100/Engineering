namespace Engineering.Application.WebServices.WarehouseServices.InvoiceProducts.Models.GetByInvoiceId;

public class GetAllActiveInvoiceProductsResponse
{
    [JsonProperty("value")]
    public GetByInvoiceIdResponseModel? Value { get; set; }
}
