namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsHistoryById;

public record GetRequestGoodsHistoryByIdResponse(
    List<GetRequestGoodsHistoryByIdDetailModel> Data,
    int? RowCount
    );

