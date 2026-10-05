using Engineering.Application.WebServices.WarehouseServices.Invoices.Models;
using Engineering.Application.WebServices.WarehouseServices.Products.Commands.CreateExitForConsumes;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.CreateExitForConsumes;

namespace Engineering.Application.WebServices.WarehouseServices.Invoices.Commands.CreateEntryThroughBorrow;

public record CreateEntryThroughBorrowCommand(
    long WarehouseId,
    string? Description,
    string? CommercialRequestId,
    string? CommercialRequestNo,
    List<InvoiceProductDto> Products,
    ImportanceDegree? Importance,
    long OwnerId,
    string? OwnerName,
    List<DocumentInvoiceModel>? Documents,
    long? UserRegisterId,
    bool IsManually = true
    ) : ICommand<CreateInvoiceResponse?>;
