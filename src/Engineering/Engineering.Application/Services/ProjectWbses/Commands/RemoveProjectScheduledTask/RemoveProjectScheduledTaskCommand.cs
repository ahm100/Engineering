using Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectScheduledTask;

namespace Engineering.Application.Services.ProjectWbses.Commands.RemoveProjectScheduledTask;

public record RemoveProjectScheduledTaskCommand(
    long TaskId) : ICommand<RemoveProjectScheduledTaskResponse?>;