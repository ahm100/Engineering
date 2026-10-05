namespace Engineering.Application.Services.ProjectOperations.Models.Models.Dependency;

public record ProjectOperationDependencyDataModel(
    long Id,
    long RelationId,
    string DependencyName,
    string DependencyCode,
    int Priority,
    int RelationDays,
    string DependencyType
    );
