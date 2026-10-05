using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;

namespace Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoByOperationInfo;

public record GetsServiceInfoByOperationInfoResponseModel(
    long Id,
    string ServiceInfoName,
    string ServiceInfoCode,
    long MeasurementId,
    string? MeasurementName,
    OperationInfoMeasurementModel MeasurementData,
    bool IsActive
    );
