namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;

public record GetsActiveOperationInfosModel(
    long Id,
    string OperationInfoName,
    string OperationInfoCode,
    string? OperationLatinName,
    long? MeasurementId,
    string? MeasurementName,
    OperationInfoMeasurementModel? MeasurementData,
    int? SetPriority
    );
