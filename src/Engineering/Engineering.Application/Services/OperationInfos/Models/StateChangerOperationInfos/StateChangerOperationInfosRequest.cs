
namespace Engineering.Application.Services.OperationInfos.Models.StateChangerOperationInfos;

public record StateChangerOperationInfosRequest(
    List<long> Ids,
    bool State
    ) : IHttpRequest;
