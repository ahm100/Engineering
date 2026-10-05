namespace Engineering.Application.Services.ProjectOperationWbses.Contracts.UpdateProjectOperationWbs;

public record UpdateProjectOperationWbsRequest(
    long Id,
    UpdateProjectOperationWbsModel Payload) : IHttpRequest;

public record UpdateProjectOperationWbsModel(
    long? ProjectWbsId,
    long? ProjectOperationId,
    bool? IsActive);