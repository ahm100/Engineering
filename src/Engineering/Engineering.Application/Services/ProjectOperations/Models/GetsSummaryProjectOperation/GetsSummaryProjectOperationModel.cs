using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.GetsSummaryProjectOperation;

public record GetsSummaryProjectOperationModel(
    long Id,
    long OperationInfoId,
    string OperationInfoName,
    string OperationInfoCode,
    int? Priority,
    long OperationInfoMeasurementId,
    string? OperationInfoMeasurementName,
    long MeasurementId,
    string? MeasurementName,
    ProjectOperationStatus Status,
    string StatusDescription,
    long? DependencyId,
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

