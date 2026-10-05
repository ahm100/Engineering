using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationDependencies.Commands.CreateProjectOperationDependency;

public record CreateProjectOperationDependencyCommand(
    long PredecessorProjectOperationId,
    long SuccessorProjectOperationId,
    int LagDays,
    ProjectOperationDependencyType DependencyType
    ) : ICommand<ProjectOperationDependency>;