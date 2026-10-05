using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetRequestGoodsSupplyProducts;

public record GetRequestGoodsSupplyProductModel
{
    public long Id { get; set; }
    public GoodsSupplyManagementStatus Status { get; set; }
    public GoodsSupplyManagementType Type { get; set; }
    public string StatusDescription => $"{Status.GetEnumDescription()}{Type.GetEnumDescription()}";
    public string TypeDescription => Type.GetEnumDescription();
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public long? ProductId { get; set; }
    public string? ProductName { get; set; } = string.Empty;
    public string? ProductCode { get; set; } = string.Empty;
    public string? ProductBrand { get; set; } = string.Empty;
    public string? ProductBrandModel { get; set; } = string.Empty;
    public string? ProductMeasure { get; set; } = string.Empty;
    public decimal RequestedCount { get; set; }
    public long? WarehouseId { get; set; }
    public long? InvoiceId { get; set; }
    public DateTime? DelivaryDeadLine { get; set; }
    public string? LastDescription { get; set; }
}
