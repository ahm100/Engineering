
namespace Engineering.Application.Services.ServiceInfos.Models.StateChangerServiceInfos;

public record InactivateServiceInfosRequest(
    List<long> Ids
    ) : IHttpRequest;
