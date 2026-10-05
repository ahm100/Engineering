using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.RequestGoodsSupplyManagements.Commands.UpdateRequestGoodsSupplyManagement;

public record UpdateRequestGoodsSupplyManagementCommand(
    long RequestGoodsSupplyManagementId,
    long? SourceWarehouseId,
    long? DestinationWarehouseId,
    long? InvoiceId,
    decimal RequestedCount,
    GoodsSupplyManagementType Type,
    string? Description,
    string? LastDescription
    ) : ICommand<RequestGoodsSupplyManagement>;
