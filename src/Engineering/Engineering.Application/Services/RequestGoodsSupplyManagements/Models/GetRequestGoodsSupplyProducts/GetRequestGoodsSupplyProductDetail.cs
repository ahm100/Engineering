namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetRequestGoodsSupplyProducts;

public record GetRequestGoodsSupplyProductDetail
{
    public List<GetRequestGoodsSupplyProductModel> Products { get; set; } = new();
    public int RowCount { get; set; }
}