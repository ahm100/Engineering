namespace Engineering.Application.Services.ProjectWbses.Contracts.EditActualStartProjectScheduleTask;

public record EditActualStartProjectScheduleTaskRequest(
    long Id,
    DateTime? DateTime) : IHttpRequest;