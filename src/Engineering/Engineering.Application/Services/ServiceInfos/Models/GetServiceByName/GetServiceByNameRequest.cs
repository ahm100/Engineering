namespace Engineering.Application.Services.ServiceInfos.Models.GetServiceByName;

public record GetServiceInfoByNameRequest(
    string ServiceInfoName
     ) : IHttpRequest;
