namespace Engineering.Application.Services.ProjectWbses.Contracts.ReSchheduledProjectSchedule;

public record ReSchheduledProjectScheduleRequest(
    long Id,
    DateTime DateTime,
    bool RescheduleUncompletedWork = true) : IHttpRequest;