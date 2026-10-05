using Engineering.Application.Services.ProjectWbses.Contracts.MoveProjectScheduledTask;

namespace Engineering.Application.Services.ProjectWbses.Commands.MoveProjectScheduledTask;

public record MoveProjectScheduledTaskCommand(
    long TaskId,
    int NewIndex) : ICommand<MoveProjectScheduledTaskResponse>;