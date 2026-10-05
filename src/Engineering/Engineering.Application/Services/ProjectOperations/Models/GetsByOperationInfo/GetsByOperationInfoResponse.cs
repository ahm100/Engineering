using Engineering.Application.Services.ProjectOperations.Models.Models;

namespace Engineering.Application.Services.ProjectOperations.Models.GetsByOperationInfo;

public record GetsByOperationInfoResponse(
    List<GetsOperationInfoModel> Data,
    int RowCount);
