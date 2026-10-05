namespace Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectSchedule;

public record CreateProjectScheduleResponse(
    long ImportId,
    DateTime ScheduleStartDate);