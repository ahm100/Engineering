namespace Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectSchedule;

public record CreateProjectScheduleRequest(
    long ProjectId,
    DateTime ScheduleStartDate) : IHttpRequest;