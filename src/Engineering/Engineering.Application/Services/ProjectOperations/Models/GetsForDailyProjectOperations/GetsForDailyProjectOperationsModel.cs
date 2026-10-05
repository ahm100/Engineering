using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.GetsForDailyProjectOperations;

public record GetsForDailyProjectOperationsModel(
    long Id,
    long OperationInfoId,
    string OperationInfoName,
    string OperationInfoCode,
    long OperationInfoMeasurementId,
    string? OperationInfoMeasurementName,
    long MeasurementId,
    string? MeasurementName,
    decimal Workload,
    long DoneWorkload,
    long RemainingWorkload,
    ProjectOperationStatus Status,
    string StatusDescription,
    DateTime? StartDate,
    DateTime? EndDate
    );
