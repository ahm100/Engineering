using Engineering.Application.WebServices.WarehouseServices.Products.Models.CreateExitForConsumes;

namespace Engineering.Application.WebServices.WarehouseServices.Invoices.Models.CreateEntryThroughBorrow;

public class CreateEntryThroughBorrowResponse
{
    [JsonProperty("value")]
    public CreateInvoiceResponse? Value { get; set; }
}
