namespace Engineering.Application.Services.ProjectOperations.Models.SetProjectOperationPriority;

public record SetProjectOperationPriorityRequest(
    long Id,
    int? Priority
     ) : IHttpRequest;
