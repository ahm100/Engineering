using Gita.Backend.Shared.Domain.Enums.Invoice;

namespace Engineering.Application.WebServices.WarehouseServices.Products.Models.CreateExitForConsumes;

public record CreateInvoiceResponse(long Id, WarehouseInvoiceType WarehouseInvoiceType, long WarehouseId, string Description,
    long? CommercialRequestId, string? CommercialRequestNo, bool IsActive, WarehouseInvoiceStatus Status, string Code);
