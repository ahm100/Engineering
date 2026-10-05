namespace Engineering.Application.Services.OperationInfos.Models.GetOIActionByOperationInfoId;

public record GetOIActionByOperationInfoIdRequest(
    long Id
     ) : IHttpRequest;
