namespace Engineering.Application.Services.OperationInfos.Models.GetOperationInfoAction;

public record GetOperationInfoActionsRequest(
    long OInfoId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;