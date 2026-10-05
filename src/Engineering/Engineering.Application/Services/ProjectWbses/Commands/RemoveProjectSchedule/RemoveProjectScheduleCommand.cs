using Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectSchedule;

namespace Engineering.Application.Services.ProjectWbses.Commands.RemoveProjectSchedule;

public record RemoveProjectScheduleCommand(
    long ProjectId) : ICommand<RemoveProjectScheduleResponse?>;