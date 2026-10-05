namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetProductsByProjectId;

public record GetProductsByProjectIdResponse(
    List<GetProductsByProjectIdModel> Data,
    int RowCount
    );

public record GetProductsByProjectIdModel
{
    public long? GroupId { get; set; }
    public string? GroupName { get; set; } = string.Empty;
    public string? GroupCode { get; set; } = string.Empty;
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
    public long? DestinationWarehouseId { get; set; }
    public bool HaveMultiProjectOperationDetail { get; set; }
    public long? CategoryId { get; set; }
    public List<GetProductsByProjectIdModelDetail>? Products { get; set; } = new();
}

public record GetProductsByProjectIdModelDetail
{
    public long Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Code { get; set; } = string.Empty;
    public string? Measure { get; set; } = string.Empty;
    public long? BrandId { get; set; }
    public string? Brand { get; set; } = string.Empty;
    public long? BrandModelId { get; set; }
    public string? BrandModel { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
    public bool? IsWastage { get; set; }
    public List<ProductFeature>? Features { get; set; }
    public float SupplyCount { get; set; }
    public float RequestedCount { get; set; }
    public float? RemainedCount => RequestedCount - SupplyCount;
}

public class ProductFeature
{
    public long? FeatureId { get; set; }
    public string? FeatureName { get; set; } = string.Empty;
    public string? Value { get; set; } = string.Empty;
}