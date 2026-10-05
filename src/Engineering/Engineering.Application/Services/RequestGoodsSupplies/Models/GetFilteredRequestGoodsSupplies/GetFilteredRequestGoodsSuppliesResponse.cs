namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetFilteredRequestGoodsSupplies;

public record GetFilteredRequestGoodsSuppliesResponse(
    List<GetFilteredRequestGoodsSuppliesModel> Data,
    int RowCount
    );

