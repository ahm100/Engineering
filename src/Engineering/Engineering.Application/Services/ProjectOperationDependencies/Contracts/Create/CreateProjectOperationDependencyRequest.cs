using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationDependencies.Contracts.Create;

public record CreateProjectOperationDependencyRequest(
    long PredecessorProjectOperationId,
    long SuccessorProjectOperationId,
    int LagDays,
    ProjectOperationDependencyType DependencyType
     ) : IHttpRequest;
