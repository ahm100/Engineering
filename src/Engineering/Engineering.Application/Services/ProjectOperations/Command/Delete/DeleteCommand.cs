using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.Delete;

public record DeleteProjectOperationCommand(
    long Id
    ) : ICommand<ProjectOperation>;