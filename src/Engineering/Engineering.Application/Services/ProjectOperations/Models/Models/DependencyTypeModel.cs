using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.Models;

public record DependencyTypeModel(
    int TypeId,
    ProjectOperationDependencyType DependencyType,
    string TypeTitle
    );
