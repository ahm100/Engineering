
namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;

public record OperationInfoDependencyModel(
    long? OperationInfoDependencyId,
    long? RelationId,
    string? DependencyName,
    string? DependencyCode,
    int? DependencyPriority,
    int WorkingDays,
    OperationInfoDependencyTypeModel TypeData
);
