using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationDependencies.Contracts.UpdateProjectOperationDependency;

public record UpdateProjectOperationDependencyRequest(
    long Id,
    UpdateProjectOperationDependencyModel Payload
     ) : IHttpRequest;

public record UpdateProjectOperationDependencyModel(
    long? PredecessorProjectOperationId,
    long? SuccessorProjectOperationId,
    int? LagDays,
    ProjectOperationDependencyType? DependencyType
     ) : IHttpRequest;
