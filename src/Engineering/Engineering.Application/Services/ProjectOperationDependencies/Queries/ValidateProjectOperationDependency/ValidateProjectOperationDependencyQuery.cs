using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationDependencies.Queries.ValidateProjectOperationDependencyQuery;

public record ValidateProjectOperationDependencyQuery(
    long? ProjectOperationId,
    DateTime? ActionDate,
    long? PredecessorId,
    long? SuccessorId,
    ProjectOperationDependencyType? Type) : IQuery<bool?>;