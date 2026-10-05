using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.SetProjectOperationPriority;

public record SetProjectOperationPriorityCommand(
    long Id,
    int? Priority
    ) : ICommand<ProjectOperation>;
