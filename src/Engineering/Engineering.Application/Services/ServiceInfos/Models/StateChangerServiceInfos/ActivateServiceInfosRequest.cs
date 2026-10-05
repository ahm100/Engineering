
namespace Engineering.Application.Services.ServiceInfos.Models.StateChangerServiceInfos;

public record ActivateServiceInfosRequest(
    List<long> Ids
    ) : IHttpRequest;
