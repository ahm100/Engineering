using Engineering.Application.Services.OperationInfoServices.Models.OperationInfoServiceModels;

namespace Engineering.Application.Services.OperationInfoServices.Models.GetsByOperationInfoId;

public record GetsByOperationInfoIdResponse(
    List<GetsByOperationInfoIdModel> Data,
    int RowCount);
