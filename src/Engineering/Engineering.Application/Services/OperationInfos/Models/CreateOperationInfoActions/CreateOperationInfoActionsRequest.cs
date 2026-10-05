namespace Engineering.Application.Services.OperationInfos.Models.CreateOperationInfoActions;

public record CreateOperationInfoActionsRequest(
    long OperationInfoId,
    decimal? IncreaseRate,
    List<CreateOperationInfoActionModel> actions) : IHttpRequest;

public record CreateOperationInfoActionModel(
    long ActionId,
    decimal? Price,
    bool IsDelete
    );