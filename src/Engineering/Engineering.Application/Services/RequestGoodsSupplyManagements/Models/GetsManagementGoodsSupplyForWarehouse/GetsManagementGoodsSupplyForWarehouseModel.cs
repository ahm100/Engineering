using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.RequestGoodsSupplyManagements.Models.GetsManagementGoodsSupplyForWarehouse;

public record GetsManagementGoodsSupplyForWarehouseModel()
{
    public long? Id { get; set; }
    public long? InvoiceId { get; set; }
    public long CostCenterId { get; set; }
    public long ProjectId { get; set; }
    public long? ProjectOperationId { get; set; }
    public long? ProjectOperationDetailId { get; set; }
    public decimal? RequestedCount { get; set; }
    public GoodsSupplyManagementType? Type { get; set; }
    public string? TypeDescription => Type?.GetEnumDescription();
    public GoodsSupplyManagementStatus? Status { get; set; }
    public string? StatusDescription => $"{Status?.GetEnumDescription()}{Type?.GetEnumDescription()}";
    public string? Description { get; set; }
    public string? LastDescription { get; set; }
}
