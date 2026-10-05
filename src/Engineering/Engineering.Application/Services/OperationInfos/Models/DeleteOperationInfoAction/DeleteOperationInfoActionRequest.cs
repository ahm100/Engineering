namespace Engineering.Application.Services.OperationInfos.Models.DeleteOperationInfoAction;

public record DeleteOperationInfoActionRequest(
    long OperationInfoId,
    long ActionId
     ) : IHttpRequest;
