namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyDetail;

public record GetsRequestGoodsSupplyDetailResponse(
    List<GetsRequestGoodsSupplyDetailModel> Data,
    int RowCount
    );
