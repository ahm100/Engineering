namespace Engineering.Application.RequestGoodsSupplyManagements.Models.GetFilteredAlternativeProducts;

public record GetFilteredAlternativeProductsResponse(
    List<GetFilteredAlternativeProductsModel> Data,
    int RowCount
    );
