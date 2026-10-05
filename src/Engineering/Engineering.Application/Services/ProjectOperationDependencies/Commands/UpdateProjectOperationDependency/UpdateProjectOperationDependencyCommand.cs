using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationDependencies.Commands.UpdateProjectOperationDependency;

public record UpdateProjectOperationDependencyCommand(
    long Id,
    long? PredecessorProjectOperationId,
    long? SuccessorProjectOperationId,
    int? LagDays,
    ProjectOperationDependencyType? DependencyType
    ) : ICommand<ProjectOperationDependency>;