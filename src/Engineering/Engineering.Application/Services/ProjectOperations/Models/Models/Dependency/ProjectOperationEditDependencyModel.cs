using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.Models.Dependency;

public record ProjectOperationEditDependencyModel(
    long? Id,
    long RelationId,
    int RelationDays,
    ProjectOperationDependencyType DependencyType
    );
