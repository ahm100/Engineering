
namespace Engineering.Application.WebServices.WarehouseServices.Invoices.Models.CreateEntryThroughBorrow;

public record CreateEntryThroughBorrowRequest(long WarehouseId, string? Description,
    string? CommercialRequestId, string? CommercialRequestNo, List<InvoiceProductDto> Products, int? Importance,
    long OwnerId, string? OwnerName, List<DocumentInvoiceModel>? Documents, bool IsManually = true);
