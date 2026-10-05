using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProducts;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyDetail;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetFilteredRequestGoodsSuppliesReports;

public record GetFilteredRequestGoodsSupplyDetailsReportsModel
{
    public long? Id { get; set; }
    public long? RequestGoodsSupplyId { get; set; }
    public long? ConsumableVolumeProductId { get; set; }
    public decimal? ProductNumber { get; set; }
    public long GroupId { get; set; }
    public string? GroupName { get; set; } = string.Empty;
    public string? GroupCode { get; set; } = string.Empty;
    public string? GroupMeasure { get; set; } = string.Empty;
    public GetProductModel? Product { get; set; }
    public long? ProductId { get; set; }
    public string? ProductName { get; set; } = string.Empty;
    public string? ProductCode { get; set; } = string.Empty;
    public string? Brand { get; set; } = string.Empty;
    public string? BrandModel { get; set; } = string.Empty;
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public string? ProjectOperationDetailDescription { get; set; } = string.Empty;
    public decimal? TolerancePercentage { get; set; } = 0;
    public decimal? ToleranceCount { get; set; } = 0;
    public decimal? TotalEstimatedCount { get; set; } = 0;
    public decimal? TotalRequestedCount { get; set; } = 0;
    public decimal? TotalSupplyCount { get; set; } = 0;
    public decimal? TotalRemainedCount { get; set; } = 0;
    public decimal? RequestedCount { get; set; } = 0;
    public decimal? SupplyCount { get; set; } = 0;
    public decimal? RemainedCount => RequestedCount - SupplyCount;
    public DateTime? DelivaryDeadLine { get; set; }
    public decimal? TotalPrice { get; set; } = 0;
    public decimal? UnitPrice { get; set; } = 0;
    public decimal? TaxPercentage { get; set; } = 0;
    public decimal? DiscountByNumber { get; set; } = 0;
    public decimal? DiscountByPercentage { get; set; } = 0;
    public decimal? FinalPrice { get; set; } = 0;
    public GoodsSupplyDetailImportance? Importance { get; set; }
    public string? ImportanceDescription => Importance?.GetEnumDescription();
    public GoodsSupplyDetailStatus? Status { get; set; }
    public string? StatusDescription => Status?.GetEnumDescription();
    public GoodsSupplyManagementType? ManagementType { get; set; }
    public string? ManagementTypeDescription => ManagementType?.GetEnumDescription();
    public string? Creator { get; set; }
    public string? Description { get; set; }
    public string? ManagementDescription { get; set; }
    public bool? CheckGroup { get; set; }
    public long? DestinationWarehouseId { get; set; }
    public List<string>? Documents { get; set; } = new();
    public long? ContractorId { get; set; }
    public string? FullName { get; set; } = string.Empty;
    public string? CustomerInvoiceNumber { get; set; }
    public string? LastDescription { get; set; }
    public RequestGetsPackagesByIdsModel? Package { get; set; }
}
