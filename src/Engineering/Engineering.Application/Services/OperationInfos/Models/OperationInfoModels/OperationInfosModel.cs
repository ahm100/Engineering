
namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;

public record OperationInfosModel(
    long Id,
    string OperationInfoName,
    string OperationInfoCode,
    string? OperationLatinName,
    int? Priority,
    bool? HaveStandard,
    long? UnitOfMeasurementId,
    string? MeasurementName,
    long? OperationInfoDependencyId,
    long? RelationId,
    string? DependencyName,
    string? DependencyCode,
    int? DependencyPriority,
    int? WorkingDays,
    string? DependencyType,
    bool IsActive
    );
