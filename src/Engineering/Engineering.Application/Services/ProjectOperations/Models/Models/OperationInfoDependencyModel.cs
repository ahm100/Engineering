using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.Models;

public record OperationInfoDependencyModel(
    long? Id,
    bool? IsDefaultRelation,
    long? RelationId,
    string? RelationName,
    string? RelationCode,
    long? RelationMeasurementId,
    string? RelationMeasurementName,
    int? RelationDays,
    OperationInfoDependencyType? Type,
    string? TypeDescription
    );
