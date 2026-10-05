namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoActions;

public record CreateOperationInfoActionRequest(
    long OperationInfoId,
    long ActionId,
    decimal? Price) : IHttpRequest;