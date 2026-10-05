using Gita.Backend.Shared.Domain.Enums.Invoice;

namespace Engineering.Application.WebServices.WarehouseServices.Products.Models.RemoveInvoice;

public record RemoveInvoiceResponseModel(
    long Id,
    WarehouseInvoiceType WarehouseInvoiceType,
    long WarehouseId,
    string Description,
    long? CommercialRequestId,
    bool IsActive);
