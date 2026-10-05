namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyProductGroups;

public record GetRequestGoodsSupplyProductGroupsResponse(
    List<GetRequestGoodsSupplyProductGroupsResponseModel> Data,
    int RowCount
    );
