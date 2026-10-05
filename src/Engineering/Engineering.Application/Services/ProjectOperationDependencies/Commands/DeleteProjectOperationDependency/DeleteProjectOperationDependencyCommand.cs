using ProjectOperationDependency = Engineering.Domain.Entities.ProjectOperations.ProjectOperationDependency;

namespace Engineering.Application.Services.ProjectOperationDependencies.Commands.DeleteProjectOperationDependency;

public record DeleteProjectOperationDependencyCommand(
    long Id
    ) : ICommand<ProjectOperationDependency>;