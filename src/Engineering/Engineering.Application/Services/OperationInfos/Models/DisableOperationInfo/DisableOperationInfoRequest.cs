namespace Engineering.Application.Services.OperationInfos.Models.DisableOperationInfo;

public record DisableOperationInfoRequest(
    long Id
     ) : IHttpRequest;
