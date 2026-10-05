namespace Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectScheduledTask;

public record RemoveProjectScheduledTaskRequest(
    long TaskId) : IHttpRequest;