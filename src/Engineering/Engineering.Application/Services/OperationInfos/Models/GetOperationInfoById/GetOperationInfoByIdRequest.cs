namespace Engineering.Application.Services.OperationInfos.Models.GetOperationInfoById;

public record GetOperationInfoByIdRequest(
    long Id
     ) : IHttpRequest;
