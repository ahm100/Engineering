using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectScheduledTask;

namespace Engineering.Application.Services.ProjectWbses.Commands.CreateProjectScheduledTask;

public record CreateProjectScheduledTaskCommand(
    long ImportId,
    long? ParentTaskId,
    string Title,
    int? SortOrder = null) : ICommand<CreateProjectScheduledTaskResponse?>;