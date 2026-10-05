using Engineering.Application.WebServices.WarehouseServices.Invoices.Models;
using Engineering.Application.WebServices.WarehouseServices.Products.Commands.CreateExitForConsumes;

namespace Engineering.Application.WebServices.WarehouseServices.Products.Models.CreateExitForRelocations;

public record CreateExitForRelocationsRequest(
    long SourceWarehouseId,
    long DestinationWarehouseId,
    int Importance,
    string CommercialRequestId,
    string CommercialRequestNo,
    List<InvoiceExitProductDtoNoManagement> Products,
    List<DocumentInvoiceModel>? Documents,
    long UserRegisterId,
    bool IsManually,
    long? OwnerId);
