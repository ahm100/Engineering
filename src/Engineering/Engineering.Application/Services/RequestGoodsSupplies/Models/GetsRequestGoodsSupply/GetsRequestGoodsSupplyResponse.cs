namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupply;

public record GetsRequestGoodsSupplyResponse(
    List<GetsRequestGoodsSupplyModel> Data,
    int RowCount
    );
