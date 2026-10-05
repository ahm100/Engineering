namespace Engineering.Application.Services.ProjectWbses.Contracts.EditProjectScheduleTaskDurationDays;
public record EditProjectScheduleTaskDurationDaysRequest(
    long TaskId,
    long? Days,
    long? Minutes) : IHttpRequest;
