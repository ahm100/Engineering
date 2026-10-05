namespace Engineering.Application.Services.ProjectOperations.Models.DeleteProjectOperationActions;

public record DeleteProjectOperationActionsRequest(
    List<long> ProjectOperationActionIds) : IHttpRequest;
