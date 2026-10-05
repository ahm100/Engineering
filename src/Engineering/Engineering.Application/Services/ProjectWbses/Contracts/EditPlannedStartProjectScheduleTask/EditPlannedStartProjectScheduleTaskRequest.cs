namespace Engineering.Application.Services.ProjectWbses.Contracts.EditPlannedStartProjectScheduleTask;

public record EditPlannedStartProjectScheduleTaskRequest(
    long Id,
    DateTime DateTime) : IHttpRequest;