namespace Engineering.Application.Services.ProjectWbses.Contracts.EditActualFinishProjectScheduleTask;

public record EditActualFinishProjectScheduleTaskRequest(
    long Id,
    DateTime? DateTime) : ICommand<EditActualFinishProjectScheduleTaskResponse>;