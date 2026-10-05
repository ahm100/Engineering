namespace Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectSchedule;

public record RemoveProjectScheduleRequest(
    long ProjectId) : IHttpRequest;