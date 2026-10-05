namespace Engineering.Application.Services.ProjectWbses.Contracts.MoveProjectScheduledTask;

public record MoveProjectScheduledTaskRequest(
    long TaskId,
    int NewIndex) : IHttpRequest;
