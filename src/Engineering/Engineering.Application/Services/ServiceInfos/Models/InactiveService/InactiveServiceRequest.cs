namespace Engineering.Application.Services.ServiceInfos.Models.InactiveService;

public record InactiveServiceInfoRequest(
    long Id
     ) : IHttpRequest;
