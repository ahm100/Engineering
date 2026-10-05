namespace Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectScheduledTask;

public record CreateProjectScheduledTaskRequest(
    long ProjectId,
    string Title,
    long? ParentTaskId,
    int? SortOrder = null) : IHttpRequest;