using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProducts;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyDetail;

public record GetsRequestGoodsSupplyDetailModel
{
    public long? Id { get; set; }
    public long? ProjectId { get; set; }
    public bool CanUpdate { get; set; }
    public long ProductId { get; set; }
    public long ProjectOperationDetailId =>
    long.Parse($"{ProjectId}{ProductGroupId ?? 0}{ProductCategoryId ?? 0}");
    public long? ProductGroupId { get; set; }
    public long? ProductCategoryId { get; set; }
    public GetsRequestGoodsSupplyDetailProjectOperationDetailModel? ProjectOperationDetail { get; set; }
    public string? ProjectOperationDetails { get; set; }
    public List<long>? ConsumableVolumeProductIds { get; set; }
    public List<long>? ProjectProductIds { get; set; }
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
    public decimal? UnitPrice { get; set; } = 0;
    public decimal? TotalPrice { get; set; } = 0;
    public decimal? DiscountedPrice { get; set; } = 0;
    public decimal? TaxPercentage { get; set; } = 0;
    public decimal? TaxNumber { get; set; } = 0;
    public decimal? DiscountByNumber { get; set; } = 0;
    public decimal? DiscountByPercentage { get; set; } = 0;
    public decimal? PackingPrice { get; set; } = 0;
    public decimal? TransferPrice { get; set; } = 0;
    public decimal? OtherPrice { get; set; } = 0;
    public decimal? FinalPrice { get; set; } = 0;
    public GoodsSupplyDetailImportance? Importance { get; set; }
    public string? ImportanceDescription => Importance?.GetEnumDescription();
    public GoodsSupplyDetailStatus? Status { get; set; }
    public string? StatusDescription => Status?.GetEnumDescription();
    public string? Creator { get; set; }
    public string? Description { get; set; }
    public string? ManagementDescription { get; set; }
    public string? LastDescription { get; set; }
    public List<string>? Documents { get; set; } = new();
    public bool? CheckGroup { get; set; }
    public long? DestinationWarehouseId { get; set; }
    public string? CustomerInvoiceNumber { get; set; }
    public GetProductModel? Product { get; set; }
    public GetsRequestGoodsSupplyDetailGroupModel? Group { get; set; }
    public RequestGetsPackagesByIdsModel? Package { get; set; }
    public RequestGoodsSupplyContractor? Contractor { get; set; }
    public List<GetsRequestGoodsSupplyDetailManagementModel>? Managements { get; set; } = new();
}

public record RequestGoodsSupplyContractor(
    long? Id,
    string? FullName
    );

public record RequestGetsPackagesByIdsModel(
    long? Id,
    decimal? Quantity,
    bool? IsDefault,
    bool? IsActive,
    string? Title,
    decimal? PackageCount
    );
