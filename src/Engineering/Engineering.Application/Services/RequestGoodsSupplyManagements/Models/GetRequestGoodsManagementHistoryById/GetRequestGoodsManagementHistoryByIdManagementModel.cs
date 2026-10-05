using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetRequestGoodsManagementHistoryById;

public record GetRequestGoodsManagementHistoryByIdManagementModel
{
    public long Id { get; set; }
    public long? ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductCode { get; set; }
    public string? Created { get; set; }
    public GoodsSupplyManagementStatus Status { get; set; }
    public string? StatusDescription => Status.GetEnumDescription();
    public GoodsSupplyManagementType Type { get; set; }
    public string? TypeDescription => Type.GetEnumDescription();
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public decimal RequestedCount { get; set; }
    public long? WarehouseId { get; set; }
    public string? Warehouse { get; set; }
    public long? InvoiceId { get; set; }
    public long? AlternateId { get; set; }
    public string? Description { get; set; }
    public string? AssignmentDate { get; set; }
}
