using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.GetsByProjectOperationId;

public record GetsByProjectOperationIdModel(
    long Id,
    long OperationInfoId,
    string OperationInfoName,
    string OperationInfoCode,
    bool HaveStandard,
    long OperationInfoMeasurementId,
    string? OperationInfoMeasurementName,
    long MeasurementId,
    string? MeasurementName,
    long? Workload,
    long? DoneWorkload,
    long? RemainingWorkload,
    decimal TolerancePercentage,
    decimal? Price,
    int? Priority,
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
    string? TypeDescription,
    string? Description,
    long? CompanyId,
    List<string>? Urls,
    string? CompanyNameFa
    );
