namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.OperationInfoDependency;

public record OperationInfoDependencyDataModel(
    long Id,
    long RelationId,
    string DependencyName,
    string DependencyCode,
    int DependencyPriority,
    int WorkingDays,
    string DependencyType
    );
