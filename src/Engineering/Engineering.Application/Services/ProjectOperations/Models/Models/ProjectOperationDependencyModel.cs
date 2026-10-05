using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.Models;

public record ProjectOperationDependencyModel(
    long? Id,
    bool? IsDefaultRelation,
    long? RelationId,
    string? RelationName,
    string? RelationCode,
    long? RelationMeasurementId,
    string? RelationMeasurementName,
    int? RelationDays,
    ProjectOperationDependencyType? Type,
    string? TypeDescription
    );
