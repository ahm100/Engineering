using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;

namespace Engineering.Application.Services.OperationInfos.Models.GetOperationInfos;

public record GetOperationInfosResponse(
    List<GetOperationInfosModel>? Data,
    int RowCount);
