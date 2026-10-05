namespace Engineering.Application.Services.OperationInfos.Models.ActiveOperationInfo;

public record ActiveOperationInfoRequest(
    long Id
     ) : IHttpRequest;
