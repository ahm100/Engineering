using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperationStatus;

public record UpdateProjectOperationStatusCommand(
    long ProjectOperationId
    ) : ICommand<ProjectOperation>;