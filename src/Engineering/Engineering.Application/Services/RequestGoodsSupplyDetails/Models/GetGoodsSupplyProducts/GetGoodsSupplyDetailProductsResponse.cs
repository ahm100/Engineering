namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProducts;

public record GetGoodsSupplyDetailProductsResponse(
    List<GetGoodsSupplyDetailProductsModel> Data,
    int RowCount
    );
