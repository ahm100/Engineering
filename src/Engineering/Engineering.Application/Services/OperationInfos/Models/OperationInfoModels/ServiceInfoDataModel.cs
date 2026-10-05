namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;

public record ServiceInfoDataModel(
    long OperationInfoServiceId,
    long Id,
    string ServiceInfoName,
    string ServiceInfoCode,
    OperationInfoMeasurementModel MeasurementData,
    string TimeSpant,
    bool IsActive
 );
