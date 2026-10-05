using Engineering.Domain.Entities.ServiceInfos.Enums;

namespace Engineering.Application.Services.ServiceInfos.Models.CreateService;

public record CreateServiceInfoRequest(
    string ServiceInfoCode,
    string ServiceInfoName,
    string? ServiceInfoEnName,
    string? DescriptionFa,
    string? DescriptionEn,
    ServiceMeasurementData? MeasurementData,
    List<long>? OperationInfoIds,
    string? TimeSpant,
    bool IsActive,
    List<string>? DocumentUrls,
    ServiceInfoType? Type = ServiceInfoType.Engineering
    ) : IHttpRequest;