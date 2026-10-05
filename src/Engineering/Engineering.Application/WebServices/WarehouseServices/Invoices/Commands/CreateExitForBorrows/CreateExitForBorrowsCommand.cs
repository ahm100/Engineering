using Engineering.Application.WebServices.WarehouseServices.Invoices.Models;
using Engineering.Application.WebServices.WarehouseServices.Products.Commands.CreateExitForConsumes;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.CreateExitForConsumes;

namespace Engineering.Application.WebServices.WarehouseServices.Invoices.Commands.CreateExitForBorrows;

public record CreateExitForBorrowsCommand(
    long WarehouseId,
    long? DestWarehouseId,
    string? Description,
    int Importance,
    string? CommercialRequestId,
    string? CommercialRequestNo,
    List<InvoiceExitProductDto>? Products,
    long OwnerId,
    string? OwnerName,
    List<DocumentInvoiceModel>? Documents,
    long? UserRegisterId,
    bool IsManually = true
    ) : ICommand<CreateInvoiceResponse?>;
