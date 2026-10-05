namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsProjectManagerRequestGoodsSupplie;

public record GetsProjectManagerRequestGoodsSupplieResponse(
    List<GetsProjectManagerRequestGoodsSupplieModel> Data,
    int RowCount
    );
