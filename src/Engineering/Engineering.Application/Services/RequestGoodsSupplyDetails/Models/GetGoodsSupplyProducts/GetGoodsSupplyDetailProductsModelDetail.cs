namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProducts;

public record GetGoodsSupplyDetailProductsModelDetail
{
    public long Id { get; set; }
    public long GroupId { get; set; } = 0;
    public string? Name { get; set; } = string.Empty;
    public string? Code { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
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
    public List<string>? Urls { get; set; }
}

public class ProductFeature
{
    public long? FeatureId { get; set; }
    public string? FeatureName { get; set; } = string.Empty;
    public string? Value { get; set; } = string.Empty;
}

public record GetProductModel
{
    public long Id { get; set; }

    public string? Brand { get; set; }

    public string? BrandModel { get; set; }

    public bool IsActive { get; set; }

    public List<ProductFeatureResponse>? Features { get; set; }

    public long? BrandId { get; set; }

    public long? BrandModelId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string? NameEn { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string? ProductDescription { get; set; }

    public GroupResponse Group { get; set; }
}