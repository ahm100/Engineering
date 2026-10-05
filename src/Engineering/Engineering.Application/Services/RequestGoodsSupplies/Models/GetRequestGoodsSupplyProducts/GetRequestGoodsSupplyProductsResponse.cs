namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyProducts;

public record GetRequestGoodsSupplyProductsResponse(
    List<GetRequestGoodsSupplyProductsResponseModel> Data,
    int RowCount
    );
