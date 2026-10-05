using Engineering.Application.WebServices.WarehouseServices.Products.Commands.CreateExitForConsumes;

namespace Engineering.Application.WebServices.WarehouseServices.Invoices.Models.CreateExitForBorrows;

public record CreateExitForBorrowRequest(long WarehouseId, string? Description, int Importance,
    string? CommercialRequestId, string? CommercialRequestNo, List<InvoiceExitProductDto>? Products, long OwnerId,
    string? OwnerName, List<DocumentInvoiceModel>? Documents, bool IsManually = true);
