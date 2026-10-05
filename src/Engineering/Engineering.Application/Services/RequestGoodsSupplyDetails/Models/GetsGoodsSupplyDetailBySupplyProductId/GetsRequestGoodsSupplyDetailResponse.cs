using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyDetail;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyDetailBySupplyProductId;

public record GetsGoodsSupplyDetailBySupplyProductIdResponse(
    List<GetsGoodsSupplyDetailBySupplyProductIdModel> Data
    );

public record GetsGoodsSupplyDetailBySupplyProductIdModel
{
    public long Id { get; set; }
    public long RequestGoodsSupplyId { get; set; }
    public long? ProjectOperationId { get; set; }
    public long? ProjectId { get; set; }
    public long? ProjectOperationDetailId { get; set; }
    public long? ConsumableVolumeProductId { get; set; }
    public long? ProjectProductId { get; set; }
    public long? ProductGroupId { get; set; }
    public long? ProductCategoryId { get; set; }
    public long? PackageId { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public string? ProjectOperationDetailDescription { get; set; } = string.Empty;
    public long? ProductId { get; set; }
    public string? ProductName { get; set; } = string.Empty;
    public string? ProductCode { get; set; } = string.Empty;
    public string? Brand { get; set; } = string.Empty;
    public string? BrandModel { get; set; } = string.Empty;
    public float? RequestedCount { get; set; } = 0;
    public DateTime? DelivaryDeadLine { get; set; }
    public decimal? ProductNumber { get; set; } = 0;
    public decimal? PackageCount { get; set; } = 0;
    public decimal? UnitPrice { get; set; } = 0;
    public decimal? TotalPrice { get; set; } = 0;
    public decimal? DiscountByNumber { get; set; } = 0;
    public decimal? DiscountByPercentage { get; set; } = 0;
    public decimal? DiscountedPrice { get; set; } = 0;
    public decimal? TaxNumber { get; set; } = 0;
    public decimal? TaxPercentage { get; set; } = 0;
    public decimal? PackingPrice { get; set; } = 0;
    public decimal? FinalPrice { get; set; } = 0;
    public string? Description { get; set; }
    public string? ManagementDescription { get; set; }
    public string? CustomerInvoiceNumber { get; set; }
    public string? LastDescription { get; set; }
    public RequestGetsPackagesByIdsModel? Package { get; set; }
}
