using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.RequestGoodsSupplyManagements.Commands.CreateRequestGoodsSupplyManagement;

public record CreateRequestGoodsSupplyManagementCommand(
    RequestGoodsSupplyProduct RequestGoodsSupplyProduct,
    long? ProductId,
    long? WarehouseId,
    long? DestinationWarehouseId,
    long? InvoiceId,
    decimal RequestedCount,
    long? AlternateId,
    GoodsSupplyManagementType Type,
    string? Description,
    string? LastDescription
    ) : ICommand<RequestGoodsSupplyManagement>;


public record CreateRequestGoodsSupplyManagementModel
{
    public required RequestGoodsSupplyProduct Detail { get; set; }
    public long? ProductId { get; set; }
    public long? WarehouseId { get; set; }
    public long? DestinationWarehouseId { get; set; }
    public long? InvoiceId { get; set; }
    public decimal RequestedCount { get; set; }
    public long? AlternateId { get; set; }
    public GoodsSupplyManagementType Type { get; set; }
    public string? Description { get; set; } = string.Empty;
};