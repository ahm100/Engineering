namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetProjectOperationDetailsByRequestId;

public record GetProjectOperationDetailsByRequestIdResponse(
    List<GetProjectOperationDetailsByRequestIdModel> Data,
    int RowCount
    );
