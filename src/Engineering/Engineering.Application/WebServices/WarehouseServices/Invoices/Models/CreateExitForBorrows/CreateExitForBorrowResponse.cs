using Engineering.Application.WebServices.WarehouseServices.Products.Models.CreateExitForConsumes;

namespace Engineering.Application.WebServices.WarehouseServices.Invoices.Models.CreateExitForBorrows;

public class CreateExitForBorrowResponse
{
    [JsonProperty("value")]
    public CreateInvoiceResponse? Value { get; set; }
}
