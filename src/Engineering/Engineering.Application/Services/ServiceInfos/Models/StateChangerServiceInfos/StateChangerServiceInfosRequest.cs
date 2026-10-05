
namespace Engineering.Application.Services.ServiceInfos.Models.StateChangerServiceInfos;

public record StateChangerServiceInfosRequest(
    List<long> Ids,
    bool State
    ) : IHttpRequest;
