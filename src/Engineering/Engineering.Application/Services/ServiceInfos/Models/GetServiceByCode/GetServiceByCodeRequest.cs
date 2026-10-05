namespace Engineering.Application.Services.ServiceInfos.Models.GetServiceByCode;

public record GetServiceInfoByCodeRequest(
    string ServiceInfoCode
     ) : IHttpRequest;
