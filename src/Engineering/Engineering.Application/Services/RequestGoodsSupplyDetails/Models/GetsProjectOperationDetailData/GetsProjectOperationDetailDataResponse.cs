namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsProjectOperationDetailData;

public record GetsProjectOperationDetailDataResponse(
    List<GetsProjectOperationDetailDataModel> Data,
    int RowCount
    );