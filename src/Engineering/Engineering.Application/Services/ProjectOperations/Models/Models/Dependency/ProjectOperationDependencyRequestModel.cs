using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.Models.Dependency;

public record ProjectOperationDependencyRequestModel(
    long PredecessorProjectOperationId,
    long SuccessorProjectOperationId,
    int LagDays,
    ProjectOperationDependencyType DependencyType
     );
