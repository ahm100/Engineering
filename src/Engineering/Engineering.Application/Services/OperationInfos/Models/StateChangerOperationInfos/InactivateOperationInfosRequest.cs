
namespace Engineering.Application.Services.OperationInfos.Models.StateChangerOperationInfos;

public record InactivateOperationInfosRequest(
    List<long> Ids
    ) : IHttpRequest;
