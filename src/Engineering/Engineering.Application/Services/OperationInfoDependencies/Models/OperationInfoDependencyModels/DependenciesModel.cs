namespace Engineering.Application.Services.OperationInfoDependencies.Models.OperationInfoDependencyModels;

public record DependenciesModel(
    long Id,
    long RelationId,
    string RelationCode,
    string RelationName,
    int WorkingDays,
    string DependencyType);
