using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProducts;

public record GetGoodsSupplyDetailProductsModel
{
    public long? UniqueId
    {
        get
        {
            long groupIdValue = GroupId ?? 0;
            long categoryIdValue = CategoryId ?? 0;
            string groupStr = groupIdValue.ToString();
            string categoryStr = categoryIdValue.ToString();
            string uniqueStr = "1" + groupStr + categoryStr;
            if (long.TryParse(uniqueStr, out long uniqueId))
                return uniqueId;
            return null;
        }
    }
    public long? GroupId { get; set; }
    public string? GroupName { get; set; } = string.Empty;
    public string? GroupCode { get; set; } = string.Empty;
    public List<string>? Urls { get; set; }
    public long? MeasureUnitId { get; set; }
    public string? GroupMeasure { get; set; } = string.Empty;
    public decimal? TotalEstimatedCount { get; set; } = 0;
    public decimal? TolerancePercentage { get; set; } = 0;
    public decimal? ToleranceCount { get; set; } = 0;
    public decimal? TotalRequestedCount { get; set; } = 0;
    public decimal? TotalSupplyCount { get; set; } = 0;
    public decimal? TotalDifferenceCount { get; set; } = 0;
    public decimal? TotalRemainedCount { get; set; } = 0;
    public decimal? RequestedCount { get; set; } = 0;
    public decimal? SupplyCount { get; set; } = 0;
    public decimal? RemainedCount => RequestedCount - SupplyCount;
    public bool AssetAccess { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsProjectSupply { get; set; }
    public long? DestinationWarehouseId { get; set; }
    public bool HaveMultiProjectOperationDetail { get; set; }
    public long? CategoryId { get; set; }
    public VolumeProductType? ProductType { get; set; }
    public List<GetGoodsSupplyDetailProductsModelDetail>? Products { get; set; } = new();
}
