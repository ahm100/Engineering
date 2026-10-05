using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;

namespace Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoByProjectOperationIds;

public record GetsServiceInfoByProjectOperationIdsResponseModel(
    long Id,
    string ServiceInfoName,
    string ServiceInfoCode,
    long MeasurementId,
    string? MeasurementName,
    OperationInfoMeasurementModel MeasurementData,
    bool IsActive,
    long? CompanyId,
    string? CompanyNameFa
    );
