using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;

namespace Engineering.Application.Services.OperationInfos.Models.GetActiveOperationInfos;

public record GetActiveOperationInfosResponse(
    List<GetsActiveOperationInfosModel> Data,
    int RowCount);
