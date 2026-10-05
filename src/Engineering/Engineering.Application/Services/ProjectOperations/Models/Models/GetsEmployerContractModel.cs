using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.Models;

public record GetsEmployerContractModel(
    long Id,
    long OperationInfoId,
    string OperationInfoName,
    string OperationInfoCode,
    bool HaveStandard,
    long OperationInfoMeasurementId,
    string? OperationInfoMeasurementName,
    long? ProjectId,
    string? ProjectName,
    string? ProjectCode,
    long? EmployerContractId,
    string? ContractCode,
    long MeasurementId,
    string? MeasurementName,
    decimal Workload,
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
    List<string>? Urls,
    string? Description
    );
