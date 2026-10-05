namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyProducts;

public record GetRequestGoodsSupplyProductsResponseModel
{
    public long? Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Code { get; set; } = string.Empty;
    public long? MeasureunitId { get; set; }
    public string? Measureunit { get; set; } = string.Empty;
};
