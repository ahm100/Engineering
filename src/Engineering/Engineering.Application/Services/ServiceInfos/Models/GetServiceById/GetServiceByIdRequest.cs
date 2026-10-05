namespace Engineering.Application.Services.ServiceInfos.Models.GetServiceById;

public record GetServiceInfoByIdRequest(
    long Id
     ) : IHttpRequest;