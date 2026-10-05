namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodSupplyRequester;

public record GetsRequestGoodSupplyRequesterResponse(
    List<GetsRequestGoodSupplyRequesterModel> Data,
    int RowCount
    );
