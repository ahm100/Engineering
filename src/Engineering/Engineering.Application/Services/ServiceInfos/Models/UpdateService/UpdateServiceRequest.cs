using Engineering.Application.Services.ServiceInfos.Models.CreateService;
using Engineering.Domain.Entities.ServiceInfos.Enums;

namespace Engineering.Application.Services.ServiceInfos.Models.UpdateService;

public record UpdateServiceInfoRequest(
    long Id,
    string ServiceInfoName,
    string? ServiceInfoEnName,
    string? DescriptionFa,
    string? DescriptionEn,
    string ServiceInfoCode,
    ServiceMeasurementData MeasurementData,
    bool IsActive,
    ServiceInfoType Type,
    List<string>? DocumentUrls
     ) : IHttpRequest;
