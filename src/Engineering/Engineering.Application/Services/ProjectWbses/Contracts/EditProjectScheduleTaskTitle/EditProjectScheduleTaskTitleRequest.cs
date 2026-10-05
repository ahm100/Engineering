namespace Engineering.Application.Services.ProjectWbses.Contracts.EditProjectScheduleTaskTitle;

public record EditProjectScheduleTaskTitleRequest(
    long TaskId,
    string Title) : IHttpRequest;