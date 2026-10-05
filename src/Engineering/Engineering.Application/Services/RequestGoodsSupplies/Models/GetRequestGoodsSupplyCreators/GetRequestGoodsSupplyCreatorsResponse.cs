namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyCreators;

public record GetRequestGoodsSupplyCreatorsResponse(
    List<GetRequestGoodsSupplyCreatorsResponseModel> Data,
    int RowCount
    );
