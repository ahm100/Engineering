
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;

namespace Engineering.Application.Services.OperationInfos.Models.GetsBySeasonId;

public record GetsBySeasonIdModel
(
    long Id,
    string OperationInfoName,
    string OperationInfoCode,
    string? OperationLatinName,
    long? MeasurementId,
    string? MeasurementName,
    OperationInfoMeasurementModel MeasurementData,
    bool IsActive
);