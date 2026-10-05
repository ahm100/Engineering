namespace Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoHistoryById;

public record GetsOperationInfoHistoryByIdRequest(
    long OperationInfoId,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
