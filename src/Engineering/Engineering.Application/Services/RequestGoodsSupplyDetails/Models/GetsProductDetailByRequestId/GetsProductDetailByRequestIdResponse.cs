namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsProductDetailByRequestId;

public record GetsProductDetailByRequestIdResponse(
    List<GetsProductDetailByRequestIdModel> Data,
    int RowCount
    );
