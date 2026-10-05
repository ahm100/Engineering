using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;

namespace Engineering.Application.Services.OperationInfos.Models.GetsByMultiFilter;

public record GetsByMultiFilterResponse(
    List<GetOperationInfosModel?> Data,
    int RowCount);
