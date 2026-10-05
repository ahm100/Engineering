
namespace Engineering.Application.Services.ProjectOperations.Models.GetsSummarizedProjectOperation;

public record GetsSummarizedProjectOperationModel(
    long ProjectOperationId,
    string OperationInfoName,
    string OperationInfoCode,
    long OperationInfoMeasurementId,
    string? OperationInfoMeasurementName,
    long UnitOfMeasurementId,
    string? MeasurementName
    );

