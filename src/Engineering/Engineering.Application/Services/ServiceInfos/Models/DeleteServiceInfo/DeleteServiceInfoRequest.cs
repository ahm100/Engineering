
namespace Engineering.Application.Services.ServiceInfos.Models.DeleteServiceInfo;

public record DeleteServiceInfoRequest(
    long Id
     ) : IHttpRequest;