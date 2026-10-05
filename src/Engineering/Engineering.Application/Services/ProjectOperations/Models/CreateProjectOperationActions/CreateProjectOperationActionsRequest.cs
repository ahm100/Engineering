namespace Engineering.Application.Services.ProjectOperations.Models.CreateProjectOperationActions;

public record CreateProjectOperationActionsRequest(
    long ProjectOperationId,
    int? IncreaseRate,
    List<CreateProjectOperationActionsModel> actions) : IHttpRequest;

public record CreateProjectOperationActionsModel(
    long OperationInfoActionId,
    decimal? Price,
    bool IsDelete
    );