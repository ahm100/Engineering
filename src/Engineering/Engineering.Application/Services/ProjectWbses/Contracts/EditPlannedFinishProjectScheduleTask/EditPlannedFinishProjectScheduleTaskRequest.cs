namespace Engineering.Application.Services.ProjectWbses.Contracts.EditPlannedFinishProjectScheduleTask;

public record EditPlannedFinishProjectScheduleTaskRequest(
    long Id,
    DateTime DateTime) : IHttpRequest;