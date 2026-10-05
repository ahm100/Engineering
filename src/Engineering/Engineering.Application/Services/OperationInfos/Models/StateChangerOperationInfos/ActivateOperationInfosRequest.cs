
namespace Engineering.Application.Services.OperationInfos.Models.StateChangerOperationInfos;

public record ActivateOperationInfosRequest(
    List<long> Ids
    ) : IHttpRequest;
