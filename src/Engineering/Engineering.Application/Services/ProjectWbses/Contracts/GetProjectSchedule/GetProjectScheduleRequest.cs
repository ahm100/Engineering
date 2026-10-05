namespace Engineering.Application.Services.ProjectWbses.Contracts.GetProjectSchedule;

public record GetProjectScheduleRequest(
    long ProjectId,
    DateTime? AsOfDate) : IHttpRequest;