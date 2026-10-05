namespace Engineering.Application.RequestGoodsSupplyManagements.Models.GetFilteredAlternativeProducts;

public record GetFilteredAlternativeProductsModel
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public long GroupId { get; set; }
    public string ProductCode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string BrandModel { get; set; } = string.Empty;
    public string Measure { get; set; } = string.Empty;
    public decimal RequestedCount { get; set; }
}
