namespace Engineering.Application.Services.ServiceInfos.Models.ActiveService;

public record ActiveServiceInfoRequest(
    long Id
     ) : IHttpRequest;
