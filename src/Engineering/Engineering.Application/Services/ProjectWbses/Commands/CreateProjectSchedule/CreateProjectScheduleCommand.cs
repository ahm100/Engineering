using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectSchedule;

namespace Engineering.Application.Services.ProjectWbses.Commands.CreateProjectSchedule;

public record CreateProjectScheduleCommand(
    long ProjectId,
    DateTime ScheduleStartDate,
    long UserId) : ICommand<CreateProjectScheduleResponse?>;