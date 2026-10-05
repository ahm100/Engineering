
namespace Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoByOperationInfo;

public record GetsServiceInfoByOperationInfoResponse(
    List<GetsServiceInfoByOperationInfoResponseModel> Data,
    int RowCount);
