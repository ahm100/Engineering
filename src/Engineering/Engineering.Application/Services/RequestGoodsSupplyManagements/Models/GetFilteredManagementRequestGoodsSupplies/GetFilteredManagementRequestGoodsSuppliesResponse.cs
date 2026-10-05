namespace Engineering.Application.RequestGoodsSupplyManagements.Models.GetFilteredManagementRequestGoodsSupplies;

public record GetFilteredManagementRequestGoodsSuppliesResponse(
    List<GetFilteredManagementRequestGoodsSuppliesModel> Data,
    int RowCount
    );
