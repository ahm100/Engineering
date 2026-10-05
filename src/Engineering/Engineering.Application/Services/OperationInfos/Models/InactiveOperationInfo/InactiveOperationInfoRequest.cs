namespace Engineering.Application.Services.OperationInfos.Models.InactiveOperationInfo;

public record InactiveOperationInfoRequest(
    long Id
     ) : IHttpRequest;
