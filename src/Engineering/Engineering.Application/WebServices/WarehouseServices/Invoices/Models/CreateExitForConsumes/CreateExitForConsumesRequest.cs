using Engineering.Application.WebServices.WarehouseServices.Invoices.Models;
using Engineering.Application.WebServices.WarehouseServices.Products.Commands.CreateExitForConsumes;

namespace Engineering.Application.WebServices.WarehouseServices.Products.Models.CreateExitForConsumes;

public record CreateExitForConsumesRequest(
    long WarehouseId,
    string? Description,
    long UserRegisterId,
    int Importance,
    string CommercialRequestId,
    string CommercialRequestNo,
    List<InvoiceExitProductDtoNoManagement> Products,
    List<DocumentInvoiceModel>? Documents,
    bool IsManually,
    long? OwnerId
    );