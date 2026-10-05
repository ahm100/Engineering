using Engineering.Domain.Entities.RequestGoodsSupplies;
using Gita.Backend.Shared.Domain.Enums.Invoice;
using Gita.Backend.Shared.Domain.Messages;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.GoodsSupplyProductExitForRelocationStatusChenger;

public record GoodsSupplyProductExitForRelocationStatusChengerCommand(
    long Id,
    long InvoiceId,
    long WarehouseId,
    WarehouseInvoiceStatus Status,
    string? Description,
    string? LastDescription,
    List<WarehouseInvoiceProductResponse> Products,
    List<WarehouseInvoiceRequestProductResponse> RequestProducts,
    List<WarehouseManagementGoodsSupplyResponse>? ManagementGoodsSupplies,
    long? UserId
    ) : ICommand<RequestGoodsSupplyProduct>;

